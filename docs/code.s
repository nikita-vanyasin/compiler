@.str = internal constant [4 x i8] c"%d\0A\00"
@.rstr = internal constant [3 x i8] c"%d\00"
declare i32 @printf(i8 *, ...)
declare i32 @scanf(i8*, ...)
@a = private global i32 0
@b = private global i32 0
define i32 @main(){
%1 = add  i32 0, 14
store i32 %1, i32* @b
br label %whilecond1
whilestart1:
%2 = load i32* @a
%3 = add  i32 0, 1
%4 = add i32 %2, %3
store i32 %4, i32* @a
br label %whilecond2
whilestart2:
%5 = load i32* @b
%6= getelementptr [4 x i8]* @.str, i64 0, i64 0
%7= call i32 (i8 *, ...)* @printf(i8* %6, i32 %5)
%8 = load i32* @b
%9 = add  i32 0, 1
%10 = sub i32 %8, %9
store i32 %10, i32* @b
br label %whilecond2
whilecond2:
%11 = load i32* @b
%12 = add  i32 0, 8
%13 = icmp sgt i32 %11, %12
%14= icmp eq i1 1, %13
br i1 %14, label %whilestart2, label %endwhile2
endwhile2:
%15 = add  i32 0, 8
store i32 %15, i32* @b
br label %whilecond1
whilecond1:
%16 = load i32* @a
%17 = add  i32 0, 2
%18 = icmp slt i32 %16, %17
%19= icmp eq i1 1, %18
br i1 %19, label %whilestart1, label %endwhile1
endwhile1:
%20 = add  i32 0, 0

ret i32 %20
}
