@.str = internal constant [4 x i8] c"%d\0A\00"
declare i32 @printf(i8 *, ...)
@temp = private global i32 0
define i32 @main(){
%1 = add i1 0, 1
%2= icmp eq i1 1, %1
br i1 %2, label %then1, label %else1
then1:
%3 = add  i32 0, 666
%4= getelementptr [4 x i8]* @.str, i64 0, i64 0
%5= call i32 (i8 *, ...)* @printf(i8* %4, i32 %3)
br label %endif1
else1:
br label %endif1
endif1:
%6 = add  i32 0, 0

ret i32 %6
}
