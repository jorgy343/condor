movl 0x0FFF, r0
movh 0x0FFF, r0
movl 0xF000, r1
movh 0xF000, r1
add r0, r1, r2

mov r2, r1
j .testlabel

.testlabel
movl r2, r1

