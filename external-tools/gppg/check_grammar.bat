@echo off
ECHO checkout last Parser.y
COPY ..\..\docs\grammar\Parser.y Parser.y
ECHO runing gppg...
CALL gppg.exe /listing Parser.y  > t_generated.cs
CALL DEL t_generated.cs
ECHO scanning listing for warnings...
CALL findstr /i /m /c:"Warning:" Parser.lst | find /c "Parser.lst"
ECHO scanning listing for errors...
CALL findstr /i /m /c:"Error:" Parser.lst | find /c "Parser.lst"
CALL DEL Parser.y


 