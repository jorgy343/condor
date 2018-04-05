.start
movl 0xFFFF, r0
movh 0xFFFF, r0
mov r0, r1
movl 0x2, r2
movh 0x0, r2
str r0, [r2]
ld [r2], r3
