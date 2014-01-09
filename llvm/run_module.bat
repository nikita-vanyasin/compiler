CALL llvm-as.exe %1 -f
CALL lli.exe %1.bc
CALL DEL %1.bc
pause