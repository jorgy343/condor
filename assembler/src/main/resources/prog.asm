movl 0xaaf0,r1
movh 0x2c44,r1
mov r1,r5
j .skip_data

.data
d32 55

.skip_data
mov r5,[r2]