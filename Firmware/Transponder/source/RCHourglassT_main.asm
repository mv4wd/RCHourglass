
; PIC16F18313 Transpoder firmware
; Developed by Marco Venturini
; Project RC Hourglass
;
; https://github.com/mv4wd/RCHourglass
;
; Licensing terms
; Short version: if you build it for your personal use, you're only kindly requested to donate a small sum to a children
; charity of your choice (use 'RC Hourglass for children' as a reference).
; 
; Please feedback your donations to:charity dot rchourglass at gmail.com
; 
; Please consider donating 5 euros per transponder and 30 euros for the decoder for personal use. 
; If the transponder is used in a club/circuit with an admission fee, please consider dontaing 100 euros for the decoder.
; 
; The original RCTech thread author/designer (Howard Cano) license applies to the decoder project:
; 
; "All information presented on this thread is free for use for personal, non-commercial purposes.
; Contact me for licensing arrangements if you wish to produceand market the decoder." -user howardcano on rctech
; 
; Additional licensing terms for the PSOC firmware/design & transponder:
; 
; "All information shared is free for use for personal, non-commercial purposes. 
; Contact me for licensing arrangements if you wish to produce and market the decoder.
; 
; The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
; Software.
; 
; Any derivative work source code/design must be public and use this licensing terms. 
; 
; Any device derived from this project must respond to the command 'License' with this text.
; 
; THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANYKIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
; WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
; IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OROTHER LIABILITY, WHETHER IN AN 
; ACTION OF CONTRACT, TORT OROTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
; SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE." Marco Venturini - mv4wd
    
    
; Assembly source line config statements

#include "p16f18313.inc"

; CONFIG1
; __config 0xD79A
 __CONFIG _CONFIG1, _FEXTOSC_HS & _RSTOSC_EXT4X & _CLKOUTEN_OFF & _CSWEN_OFF & _FCMEN_OFF
; CONFIG2
; __config 0xFFF2
 __CONFIG _CONFIG2, _MCLRE_OFF & _PWRTE_OFF & _WDTE_OFF & _LPBOREN_OFF & _BOREN_ON & _BORV_LOW& _STVREN_ON & _DEBUG_OFF ;  & _PPS1WAY_ON 
; CONFIG3
; __config 0x2003
 __CONFIG _CONFIG3, _WRT_OFF & _LVP_ON
; CONFIG4
; __config 0x3
 __CONFIG _CONFIG4, _CP_OFF & _CPD_OFF
 
SH_MEM        UDATA_SHR
MessageRam  RES   D'14'      ; RAM memory message


;Variables definitions
VARIABILI_BANKED        UDATA
d1      RES     1      ;Counter for delay
d2      RES     1      ;Counter for delay
rd1     RES     1      ;Counter for delay random
led1    RES     1      ;Counter for led toggle  
led2    RES     1      ;Counter for led toggle  
packets    RES     1      ;Counter for number of packets sent
statusid   RES     1      ;Counter for number of packets sent    

; Macro to sent one bit
SENDONEBIT MACRO addr
    NOP
    LSLF  addr, 1
    BTFSC STATUS, C  ; // jump if bit is 0 ... modulation bit doesn't change
    XORWF MDCON , 1    ; // invert modulation bit (if bit is 1)
    ;XORWF PORTA , 1    ; // invert debug output
    endm


; Macro to send one byte addressed by addrb. Warning: original byte is cleared!
SENDONEBYTE MACRO addrb
    SENDONEBIT addrb
    SENDONEBIT addrb
    SENDONEBIT addrb
    SENDONEBIT addrb
    SENDONEBIT addrb
    SENDONEBIT addrb
    SENDONEBIT addrb
    SENDONEBIT addrb
    endm




 
 

RES_VECT  CODE    0x0000            ; processor reset vector
    GOTO    SETUP                   ; go to beginning of program

; NO INTERRUPTS ARE USED
    CODE    0x0004           ; interrupt vector location

;  Empty ISR
     RETFIE
    

;*******************************************************************************
; MAIN PROGRAM
;*******************************************************************************

MAIN_PROG CODE                      ; let linker place main program

SETUP
    BANKSEL TRISA ;
    MOVLW B'00111111' ;   Startup all ports as inputs (before setting the outputs)
    MOVWF TRISA ;
    
    BANKSEL SLRCONA   ; no slew rate limiting on differential outputs, slew rate limit on led
    
    MOVLW B'00000100' ;  
    MOVWF  SLRCONA 
 
    BANKSEL CLKRCON
    ; Setup  clock reference to a 5MHz 50% duty cycle extraced by dividing by 4 main clock
    MOVLW b'10010010' ; CLKREN |CLKRDC1 ; Divide 4 by because crystal clock is 4x by PLL 
    MOVWF CLKRCON

    ; Digital Signal Modulator setup
    BANKSEL MDCON    
    CLRF MDCON         ;  Clear all

    

    ; Setup Modulation source
    CLRF MDSRC ; MDBIT bit of MDCON register is modulation source (software driven)


    ; Setup carrier High
     
    MOVLW b'00100011' ; MDCHSYNC | b'0011' ; // Carrier high is sync'd reference clock
    
    MOVWF MDCARH

    ; Setup carrier Low reverted   
    MOVLW b'01100011' ;  MDCLSYNC  | MDCLPOL | b'0011' ; // Carrier high is reversed sync'd reference clock
    
    MOVWF MDCARL
    
    ; END of Digital Signal Modulator setup
    
    
    ; Setup CLC1 = DSM out 
           
    ; Setup CLC1CON output to reproduce DSM out 
    BANKSEL CLC1CON
    MOVLW b'00000100'  ; 
    MOVWF   CLC1CON    ; CLC disabled function is D flip flop with set and reset
    
    MOVLW b'00001010'  ; SEL0 input is DSM output
    MOVWF   CLC1SEL0   
    MOVLW b'00011111'
    MOVWF   CLC1SEL1   ; SEL 1 input is FOSC clock
    MOVWF   CLC1SEL2   ; SEL 2-3 doesn't matter
    MOVWF   CLC1SEL3
    MOVLW b'00000000'  ;
    MOVWF   CLC1POL    ; Output are not reversed
    
    MOVLW b'00001000'
    MOVWF   CLC1GLS0    ; gate 1 is the FOSC signal (FF clock)
    MOVLW b'00000010'
    MOVWF   CLC1GLS1    ; gate 2 is the DSM signal (FF D)
     
    CLRF    CLC1GLS2    ; FF set = 0
    CLRF    CLC1GLS3    ; FF reset = 0
    
   
    ; END Setup CLC1 
    
    
    ; Setup CWG 
    BANKSEL CWG1CON0
    BCF     CWG1CON0, 7 ; Disable
    MOVLW b'00000100' ; CWG1 enabled works in half bridge mode
    MOVWF   CWG1CON0
    CLRF    CWG1CON1    ; Output polarity is standard
    CLRF    CWG1CLKCON  ;  Clock source is fosc
    
    CLRF  CWG1DBR     ; no raising deadband
    CLRF  CWG1DBF     ; no falling deadband
    CLRF  CWG1AS1     ; no auto shutdown
    CLRF  CWG1STR     ; no steeringg
    MOVLW b'00001010' ; CWG1  input is CLC1 output
    MOVWF CWG1DAT
    MOVLW b'10101000' ; CWG1  outputs are LOW when shutdown, CWG1 is shut down, without auto restart
    MOVWF CWG1AS0
    
    ; End of setup CWG 
    
    
    
    ; Assign output pins
    ; suspend interrupts
    bcf INTCON,GIE
    banksel PPSLOCK ; [Bank 29] (Ensure Interrupts are disabled first)
    movlw  0x55
    movwf  PPSLOCK
    movlw  0xAA
    movwf  PPSLOCK
    bcf    PPSLOCK, PPSLOCKED ; Unlocks PPS
    
    
    BANKSEL RA0PPS
     
    MOVLW b'00001000' ; RA0 output = CWG1A
     

    MOVWF RA0PPS
    
    BANKSEL RA1PPS
     
    MOVLW b'00001001' ; RA1 output = CWG1B
     
    MOVWF RA1PPS
    
    
    BANKSEL PPSLOCK ; set bank
    ; required sequence, next 5 instructions
    movlw 0x55
    movwf PPSLOCK
    movlw 0xAA
    movwf PPSLOCK
    ; Set PPSLOCKED bit to disable writes or
    ; Clear PPSLOCKED bit to enable writes
    bsf PPSLOCK,PPSLOCKED
    ; restore interrupts
    bsf INTCON,GIE 
    
    
    BANKSEL CWG1CON0
    BSF   CWG1CON0, 7 ; ENABLE
    
    BANKSEL PORTA ;
    CLRF PORTA ;Init PORTA
    BANKSEL LATA ;Data Latch
    CLRF LATA ;
    BANKSEL ANSELA ;
    CLRF ANSELA ;digital I/O
    
    BANKSEL ODCONA ; // No open drain drive. All ports are push pull drive 
    CLRF   ODCONA;  
    BANKSEL TRISA ;
    MOVLW B'00001000' ;   RA3 is always an input. Enable all outputs
    MOVWF TRISA ;
    BANKSEL WPUA ;       Weak pull up enabled on port RA3
    MOVLW B'00001000' ;   
    MOVWF WPUA ;    


    BANKSEL rd1            ; value used for  pseudo-random  delay
    MOVLW B'00010001'
    MOVWF rd1;
    BANKSEL led1
    MOVLW 0x01
    MOVWF led1;
    MOVWF led2;

    BANKSEL packets ;Counter for number of packets sent
    MOVLW   0x04
    MOVWF   packets
    BANKSEL statusid  ;Counter for status record sent 
    CLRF    statusid

MAINLOOP

    MOVLW LOW  MessageRam
    MOVWF FSR0L

    MOVLW HIGH MessageRam
    MOVWF FSR0H

    BANKSEL packets
    DECF  packets
   
    
    BTFSC STATUS,Z
    GOTO  PREPARESTATUS
    
    MOVLW LOW Message1
    MOVWF FSR1L
    MOVLW HIGH Message1
    MOVWF FSR1H
    GOTO  COPYLOOP

PREPARESTATUS
    
    BANKSEL packets ;Reset counter for number of packets sent
    MOVLW   0x04
    MOVWF   packets

    BANKSEL statusid  ;Counter for status record sent 
    INCF    statusid
    MOVF    statusid, 0
    BANKSEL d1
    MOVWF   d1
    DECFSZ  d1
    GOTO    ST2
    
    MOVLW LOW Status1
    MOVWF FSR1L
    MOVLW HIGH Status1
    MOVWF FSR1H
    GOTO  COPYLOOP
    
    
ST2 DECFSZ  d1
    GOTO    ST3
    
    MOVLW LOW Status2
    MOVWF FSR1L
    MOVLW HIGH Status2
    MOVWF FSR1H
    GOTO  COPYLOOP
    
ST3 DECFSZ  d1
    GOTO    ST4
    
    MOVLW LOW Status3
    MOVWF FSR1L
    MOVLW HIGH Status3
    MOVWF FSR1H
    GOTO  COPYLOOP
   
ST4 DECFSZ  d1
    GOTO    ST5
    
    MOVLW LOW Status4
    MOVWF FSR1L
    MOVLW HIGH Status4
    MOVWF FSR1H
    GOTO  COPYLOOP    
    
ST5 DECFSZ  d1
    GOTO    ST6
    
    MOVLW LOW Status5
    MOVWF FSR1L
    MOVLW HIGH Status5
    MOVWF FSR1H
    GOTO  COPYLOOP  
   
ST6 DECFSZ  d1
    GOTO    ST7
    
    MOVLW LOW Status6
    MOVWF FSR1L
    MOVLW HIGH Status6
    MOVWF FSR1H
    GOTO  COPYLOOP
    
ST7 
    BANKSEL statusid  ; last status byte sent. Reset counter
    CLRF statusid
    
    MOVLW LOW Status7
    MOVWF FSR1L
    MOVLW HIGH Status7
    MOVWF FSR1H
    GOTO  COPYLOOP     
    
COPYLOOP   ; Copy in RAM from address FSR1 to address FSR0. At the end the ram message is zeroed!!

    banksel d1
	;copy 12 bytes
	movlw	0x0C
	movwf	d1

COPYNEXTBYTE
    MOVIW FSR1 ++
    MOVWI FSR0 ++
    DECF  d1
    BTFSS STATUS,Z
    GOTO  COPYNEXTBYTE
    
ENDCOPY
    
    
    MOVLW HIGH MessageRam
    MOVWF FSR0H
    MOVLW LOW MessageRam
    MOVWF FSR0L


    ;BANKSEL PORTA
    ;BSF     PORTA, 2 ; // Write high on debug trigger


    
    BANKSEL CLC1CON    
    BSF     CLC1CON, 7 ; CLC1 ENABLE     
     
    
    
    BANKSEL MDCON
    MOVLW 0x01   ; // Mask to xor the output bit


    BCF  MDCON, MDBIT                 ; Clear modulation bit
    BSF  MDCON, MDEN                 ; Enable DSM
    
    BANKSEL CWG1AS0  
    BCF     CWG1AS0 , SHUTDOWN    ; clear CWG shutdown
    
    BANKSEL MDCON  
    NOP                           ; 2 full cycles
    NOP
    NOP

    NOP                           ; 2 full cycles
    NOP
    NOP
    NOP


    
SENDID

    SENDONEBYTE (MessageRam+0)
    SENDONEBYTE (MessageRam+1)
    SENDONEBYTE (MessageRam+2)
    SENDONEBYTE (MessageRam+3)
    SENDONEBYTE (MessageRam+4)
    SENDONEBYTE (MessageRam+5)
    SENDONEBYTE (MessageRam+6)
    SENDONEBYTE (MessageRam+7)
    SENDONEBYTE (MessageRam+8)
    SENDONEBYTE (MessageRam+9)
    SENDONEBYTE (MessageRam+D'10')
    SENDONEBYTE (MessageRam+D'11')


    BCF  MDCON, MDBIT                 ; Clear polarity

    NOP
    NOP                           ; 3 full cycles
    NOP
    NOP

    NOP                           ; 
    NOP
    NOP
    NOP

    NOP                           ;
    NOP
    ;NOP
    
    ;BANKSEL CLC2CON    
    ;BCF     CLC2CON, 7 ; CLC2 DISABLE    
    ;BCF     CLC1CON, 7 ; CLC1 DISABLE    
    
    BANKSEL CWG1AS0  
    BSF     CWG1AS0 , SHUTDOWN    ; clear CWG shutdown
    
    
    BANKSEL MDCON
    BCF  MDCON, MDBIT                 ; Clear polarity

    ;BANKSEL PORTA
    ;BCF     PORTA, 2                  ; Negative edge on packet trigger

    

DELAY    ;  random delay between two ID

; Delay = 0.002 seconds
; Clock frequency = 20 MHz

; Actual delay = 0.002 seconds = 10000 cycles
; Error = 0 %


    banksel d1
	;9998 cycles
	movlw	0xCF
	movwf	d1
	movlw	0x08
	movwf	d2

Delay_0
	decfsz	d1, f
	goto	$+2
	decfsz	d2, f
	goto	Delay_0

			;2 cycles
	goto	$+1

    ; Additional delay random ; Delay = 0.0001 0.0002 0.0004 0.0008 seconds
    ; total delay from  2 to 2.8 ms
Random_Delay
    BANKSEL rd1
    movfw	rd1
    andlw   0x0F
	movwf	d2

Delay_L
	; Actual delay = 0.0001 seconds = 500 cycles
    ;499 cycles
	movlw	0xA6
	movwf	d1
Delay_X
	decfsz	d1, f
	goto	Delay_X
    decfsz	d2, f
	goto	Delay_L

    RRF     rd1, 1
    BTFSC   STATUS, C
    BSF     rd1, 7

    BANKSEL led1
    INCFSZ  led1 , 1
    GOTO MAINLOOP

    MOVLW   0x7F
    MOVWF   led1

    BANKSEL PORTA  ; TOGGLE the led
    BCF     PORTA, 2

    BANKSEL led2

    DECFSZ  led2, 1
    GOTO    MAINLOOP
    
    MOVLW   0x4
    MOVWF   led2
    BANKSEL PORTA  ; TOGGLE the led
    BSF     PORTA, 2

    GOTO MAINLOOP
    

  
  

  
  
  
DATAMAP CODE 0x0700
RecordBegin DT "MV" 
RecordType DB 0x1 

  
  
; 2351957
Message1 DT 0XF9, 0X16, 0XE1, 0XCB, 0X12, 0X1C, 0XC9, 0XD6, 0XC3, 0XE0, 0XFF, 0X0F
Status1 DT 0XF9, 0X16, 0XDA, 0XE7, 0X94, 0X77, 0XE9, 0X3C, 0X91, 0XD7, 0XC3, 0XCC
Status2 DT 0XF9, 0X16, 0XEC, 0X50, 0X55, 0X92, 0XE2, 0X23, 0X61, 0XD4, 0XF0, 0X0C
Status3 DT 0XF9, 0X16, 0X36, 0X58, 0X15, 0X1B, 0XC8, 0XC3, 0X62, 0X14, 0X3C, 0X00
Status4 DT 0XF9, 0X16, 0X0E, 0X29, 0XBA, 0XE0, 0X3E, 0XE3, 0X62, 0XDB, 0XC0, 0XC3
Status5 DT 0XF9, 0X16, 0X36, 0X55, 0X57, 0X09, 0XFB, 0X3F, 0X91, 0X27, 0X00, 0XF0
Status6 DT 0XF9, 0X16, 0X0E, 0XFE, 0XF0, 0X8A, 0X22, 0X3C, 0X52, 0X1B, 0X3F, 0XF3
Status7 DT 0XF9, 0X16, 0XD7, 0XA8, 0X10, 0X77, 0XD1, 0X23, 0XA2, 0XD7, 0XC3, 0X3C  
 
 
; 2688158
;Message1 DT 0XF9, 0X16, 0X35, 0XE4, 0XF4, 0XC4, 0XFC, 0XF8, 0XF7, 0XE3, 0X00, 0X3C
;Status1 DT 0XF9, 0X16, 0X38, 0X47, 0X1B, 0X64, 0X5F, 0X01, 0X60, 0XD6, 0XCF, 0XFC
;Status2 DT 0XF9, 0X16, 0XEF, 0X38, 0X9B, 0X72, 0XB7, 0X21, 0X90, 0X15, 0X33, 0X03
;Status3 DT 0XF9, 0X16, 0X3B, 0XC0, 0XE0, 0XB2, 0XBD, 0X11, 0XA3, 0XE5, 0XCF, 0X3F
;Status4 DT 0XF9, 0X16, 0X0D, 0X98, 0XF8, 0XD0, 0X73, 0XC1, 0X5F, 0XD5, 0XC3, 0X03
;Status5 DT 0XF9, 0X16, 0X3B, 0XCD, 0XAF, 0XD7, 0XA2, 0XCE, 0XAC, 0X29, 0X00, 0X0F
;Status6 DT 0XF9, 0X16, 0X0D, 0X9B, 0X4A, 0X20, 0X5F, 0X12, 0X60, 0XE5, 0X03, 0X03
;Status7 DT 0XF9, 0X16, 0X38, 0XAB, 0XA7, 0X54, 0X71, 0X1D, 0X90, 0X15, 0X0C, 0X0F

; 3073479 
;Message1 DT 0XF9, 0X16, 0XDA, 0X30, 0XE5, 0X07, 0XC9, 0XD2, 0X1B, 0XD3, 0XF0, 0X0C
;Status1 DT 0XF9, 0X16, 0X38, 0X71, 0X40, 0XC3, 0XB9, 0X92, 0X6A, 0X1F, 0X0C, 0XF0
;Status2 DT 0XF9, 0X16, 0XEF, 0XDA, 0XEC, 0X81, 0X96, 0X71, 0X69, 0X2C, 0X33, 0X00
;Status3 DT 0XF9, 0X16, 0X35, 0XE7, 0X7D, 0XCC, 0X98, 0X6E, 0XA5, 0XD3, 0X30, 0X3C
;Status4 DT 0XF9, 0X16, 0X0D, 0X95, 0X58, 0X61, 0X7E, 0X61, 0X65, 0X1C, 0X33, 0XCF
;Status5 DT 0XF9, 0X16, 0X3B, 0X19, 0X83, 0XB8, 0X93, 0X62, 0X66, 0XE0, 0X3C, 0X33
;Status6 DT 0XF9, 0X16, 0X03, 0XBC, 0X0E, 0XDC, 0X6F, 0X7D, 0X66, 0XE3, 0XCF, 0XCC
;Status7 DT 0XF9, 0X16, 0X36, 0X8C, 0XE3, 0X7C, 0X81, 0XA1, 0X9A, 0XD3, 0X3F, 0X3F
 

; 4572215
;Message1 DT 0XF9, 0X16, 0XDA, 0X05, 0X0F, 0X38, 0XCE, 0X17, 0X23, 0XF8, 0XC0, 0XC0
;Status1 DT 0XF9, 0X16, 0XDA, 0X05, 0X39, 0X81, 0XF2, 0X8B, 0X9C, 0X1B, 0XC3, 0X3F
;Status2 DT 0XF9, 0X16, 0XE1, 0X29, 0X8A, 0XD4, 0X16, 0X78, 0X93, 0X14, 0XCC, 0XFF
;Status3 DT 0XF9, 0X16, 0X36, 0X6D, 0XF1, 0XEF, 0XEE, 0XA7, 0X5C, 0X1B, 0X03, 0XCF
;Status4 DT 0XF9, 0X16, 0XE2, 0X4F, 0XB9, 0X99, 0X02, 0X84, 0X6C, 0X27, 0X33, 0XFF
;Status5 DT 0XF9, 0X16, 0X35, 0X3D, 0X9A, 0XB7, 0X35, 0X87, 0X9F, 0X27, 0X30, 0XC0
;Status6 DT 0XF9, 0X16, 0XE2, 0XAD, 0X14, 0X8E, 0X0B, 0X98, 0X93, 0X24, 0X00, 0X3C
;Status7 DT 0XF9, 0X16, 0X38, 0XA5, 0X54, 0XD3, 0X3B, 0X9B, 0XA3, 0X28, 0X30, 0XCC 
 
; 4748687
;Message1 DT 0XF9, 0X16, 0XDA, 0XDF, 0X0A, 0X37, 0X19, 0X03, 0X34, 0X04, 0X30, 0XCC
;Status1 DT 0XF9, 0X16, 0X00, 0XE2, 0X9B, 0X79, 0XA6, 0XB2, 0X42, 0X09, 0X8C, 0XF3
;Status2 DT 0XF9, 0X16, 0X0E, 0X27, 0X44, 0XCA, 0XBC, 0X82, 0XB2, 0XF6, 0X73, 0XF0
;Status3 DT 0XF9, 0X16, 0X36, 0X58, 0X23, 0X75, 0XB3, 0XAE, 0XBE, 0X36, 0X40, 0XCF
;Status4 DT 0XF9, 0X16, 0X0D, 0X4C, 0X03, 0XF8, 0X6A, 0X6D, 0XB2, 0XCA, 0X4C, 0X3C
;Status5 DT 0XF9, 0X16, 0X36, 0XBA, 0XB8, 0XE0, 0X7E, 0XB2, 0X4D, 0X05, 0X83, 0X33
;Status6 DT 0XF9, 0X16, 0XEF, 0X0E, 0XCD, 0X43, 0X62, 0XA2, 0X8D, 0XF9, 0X40, 0X33
;Status7 DT 0XF9, 0X16, 0XD7, 0XA5, 0X52, 0XBC, 0XB9, 0X7D, 0XBE, 0XC6, 0XBF, 0XFC 
 
; 4961721
;Message1 DT 0XF9, 0X16, 0XEF, 0XDA, 0X35, 0X3B, 0X28, 0X29, 0X0B, 0X0B, 0XCF, 0X3C
;Status1 DT 0XF9, 0X16, 0X38, 0X9D, 0X13, 0XF0, 0XCC, 0XFE, 0XDD, 0X83, 0X8F, 0X3C
;Status2 DT 0XF9, 0X16, 0X36, 0X5B, 0X70, 0XAC, 0X2D, 0X02, 0XD2, 0XBC, 0X80, 0XF0
;Status3 DT 0XF9, 0X16, 0X38, 0X4A, 0X61, 0X32, 0X12, 0X2E, 0X11, 0X80, 0X4F, 0X3F
;Status4 DT 0XF9, 0X16, 0X03, 0X68, 0XC3, 0XA1, 0XCE, 0XEE, 0X2E, 0X8C, 0X43, 0X00
;Status5 DT 0XF9, 0X16, 0XEF, 0XD4, 0X1C, 0XB4, 0X2C, 0XED, 0XD2, 0XBF, 0X8C, 0XFC
;Status6 DT 0XF9, 0X16, 0XD4, 0X19, 0X5C, 0X5B, 0X2D, 0X2D, 0X11, 0X8C, 0X40, 0X0F
;Status7 DT 0XF9, 0X16, 0XDA, 0X33, 0X57, 0XF7, 0XDE, 0X2E, 0X1E, 0XB0, 0X43, 0X0F
 
; 5564690
;Message1 DT 0XF9, 0X16, 0X3B, 0XFB, 0XC2, 0XF1, 0X0A, 0XDC, 0X12, 0X38, 0XC3, 0XF0
;Status1 DT 0XF9, 0X16, 0XD7, 0X7C, 0X32, 0X04, 0X85, 0X58, 0XF5, 0X92, 0X30, 0XFC
;Status2 DT 0XF9, 0X16, 0XD4, 0X22, 0X4B, 0XC7, 0XA3, 0X88, 0X3A, 0X9E, 0X0C, 0XC0
;Status3 DT 0XF9, 0X16, 0X36, 0XB4, 0X73, 0X2D, 0X56, 0X58, 0XF9, 0X51, 0XFC, 0XF3
;Status4 DT 0XF9, 0X16, 0XDA, 0X3E, 0X23, 0X64, 0XAD, 0X74, 0XC9, 0X51, 0XFC, 0X33
;Status5 DT 0XF9, 0X16, 0X35, 0XD2, 0X4E, 0X4A, 0X4A, 0X5B, 0XF5, 0X91, 0X3F, 0X0C
;Status6 DT 0XF9, 0X16, 0XD9, 0XB7, 0X13, 0X90, 0XBD, 0X57, 0X3A, 0X91, 0X03, 0XF3
;Status7 DT 0XF9, 0X16, 0XD7, 0X71, 0X48, 0X8B, 0X90, 0X64, 0XC5, 0X52, 0XFF, 0XFF 
 
; 6000513
;Message1 DT 0XF9, 0X16, 0XEF, 0X00, 0X05, 0XE5, 0X00, 0XD9, 0X35, 0X0B, 0XF0, 0X3C
;Status1 DT 0XF9, 0X16, 0XE1, 0XC6, 0XBC, 0X89, 0X3C, 0X6B, 0X15, 0XF8, 0XB3, 0X3C
;Status2 DT 0XF9, 0X16, 0X03, 0X68, 0XC3, 0X94, 0X29, 0XA8, 0X19, 0X34, 0X7C, 0XCF
;Status3 DT 0XF9, 0X16, 0XD9, 0XB4, 0X7B, 0X89, 0XC3, 0XA8, 0X15, 0XC7, 0X73, 0X3C
;Status4 DT 0XF9, 0X16, 0X0D, 0XA3, 0X0D, 0X00, 0X21, 0X47, 0XE9, 0X38, 0X40, 0XFF
;Status5 DT 0XF9, 0X16, 0XD4, 0X2C, 0X5A, 0XE3, 0X38, 0X67, 0X19, 0X37, 0XB3, 0XC3
;Status6 DT 0XF9, 0X16, 0X0D, 0X7A, 0X8F, 0XC1, 0XFF, 0X64, 0XD6, 0XCB, 0X80, 0XFC
;Status7 DT 0XF9, 0X16, 0XDA, 0XDF, 0X07, 0X7B, 0X19, 0X44, 0XDA, 0X34, 0X8C, 0XF0 
 
; 6091953
;Message1 DT 0XF9, 0X16, 0XEF, 0X35, 0X3B, 0XFB, 0X2A, 0XD3, 0X15, 0X07, 0XFC, 0XFC
;Status1 DT 0XF9, 0X16, 0X36, 0X63, 0XE3, 0XA3, 0XB5, 0X60, 0XB9, 0X0E, 0XB3, 0X3F
;Status2 DT 0XF9, 0X16, 0X38, 0XA5, 0XB8, 0X59, 0X87, 0X8F, 0X79, 0X3E, 0XB3, 0XC3
;Status3 DT 0XF9, 0X16, 0X35, 0XEA, 0X3C, 0XB8, 0X95, 0X93, 0X7A, 0XFE, 0X7C, 0XC0
;Status4 DT 0XF9, 0X16, 0XE1, 0XC6, 0X89, 0XBA, 0X55, 0X60, 0X45, 0XCD, 0XBF, 0X30
;Status5 DT 0XF9, 0X16, 0X3B, 0XCD, 0X4E, 0X1C, 0X5C, 0X8F, 0X4A, 0X0E, 0XB3, 0X03
;Status6 DT 0XF9, 0X16, 0X38, 0X47, 0XC2, 0XD3, 0X77, 0XB3, 0X4A, 0XCE, 0X43, 0XF3
;Status7 DT 0XF9, 0X16, 0X36, 0X81, 0X4D, 0XE9, 0X96, 0XBF, 0X4A, 0X31, 0XB3, 0X0F 
 
; 6099608
;Message1 DT 0XF9, 0X16, 0X00, 0XD4, 0XFB, 0X13, 0XEF, 0XF8, 0X1A, 0X38, 0X3C, 0XF0
;Status1 DT 0XF9, 0X16, 0XD9, 0X81, 0X73, 0XF7, 0X83, 0X3F, 0XBC, 0XF2, 0XBF, 0X03
;Status2 DT 0XF9, 0X16, 0XEF, 0XEC, 0X6E, 0X7E, 0X8F, 0XF3, 0X4F, 0X0E, 0X8F, 0X33
;Status3 DT 0XF9, 0X16, 0X38, 0X9D, 0XFF, 0X41, 0X64, 0X10, 0X8C, 0XF2, 0X80, 0XFC
;Status4 DT 0XF9, 0X16, 0X0D, 0X4C, 0X36, 0XC8, 0XBC, 0XEC, 0X4F, 0XF2, 0X80, 0X3C
;Status5 DT 0XF9, 0X16, 0X3B, 0X2F, 0X0F, 0X82, 0XB7, 0X1F, 0XB3, 0X0D, 0XBF, 0XC3
;Status6 DT 0XF9, 0X16, 0X00, 0X36, 0X6D, 0XC9, 0X93, 0X03, 0X43, 0X02, 0X83, 0XCC
;Status7 DT 0XF9, 0X16, 0XD9, 0X60, 0X5A, 0X9C, 0X7D, 0X1C, 0X40, 0X3D, 0X7F, 0X0F 
 
 
; 7531106
;Message1 DT 0XF9, 0X16, 0X3B, 0XCE, 0X13, 0XE1, 0X3A, 0XD1, 0XF2, 0X17, 0X3C, 0X3C
;Status1 DT 0XF9, 0X16, 0XD9, 0X55, 0X5C, 0X2A, 0XAE, 0X38, 0XB9, 0X99, 0XCF, 0X0F
;Status2 DT 0XF9, 0X16, 0XE1, 0X27, 0X77, 0XA3, 0X51, 0XD4, 0XB9, 0X69, 0XC0, 0X00
;Status3 DT 0XF9, 0X16, 0X38, 0X44, 0XA9, 0XA1, 0X43, 0XE8, 0XB5, 0X59, 0X0F, 0X3C
;Status4 DT 0XF9, 0X16, 0X03, 0X89, 0XDF, 0X16, 0XB8, 0X08, 0X75, 0X69, 0XCC, 0X0C
;Status5 DT 0XF9, 0X16, 0X35, 0XE7, 0X92, 0X18, 0X84, 0XD4, 0X7A, 0X55, 0XC0, 0XC3
;Status6 DT 0XF9, 0X16, 0X0E, 0X1F, 0XDA, 0XBF, 0XAB, 0XF8, 0X46, 0X95, 0X3C, 0X03
;Status7 DT 0XF9, 0X16, 0XD7, 0X49, 0X34, 0X5D, 0X5B, 0XD7, 0X4A, 0X95, 0X0F, 0XCC
 
RecordEnd DT "MV"  
    END