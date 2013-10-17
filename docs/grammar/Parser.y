%namespace PySharpGrammarCheck 
%token


abstract 	as 	base 	bool 	break 	byte 	case
catch 	char 	checked 	class 	const 	continue 	decimal
default 	delegate 	do 	double 	else 	enum 	event
explicit 	extern 	false 	finally 	fixed 	float 	for
foreach 	goto 	if 	implicit 	in 	int 	interface
internal 	is 	lock 	long 	namespace 	new 	null
object 	operator 	out 	override 	params 	private 	protected
public 	readonly 	ref 	return 	sbyte 	sealed 	short
sizeof 	stackalloc 	static 	string 	struct 	switch 	this
throw 	true 	try 	typeof 	uint 	ulong 	unchecked
unsafe 	ushort 	using 	virtual 	void 	volatile 	while


%start A
%%
        
A : DAY B KESHE_1
  | B
  ;

B : G Bs ;

G : SVOBODU
  | C
  ;

Bs : KESHE C Bs
   | /* eps */
   ;

F : NU D TI
  | PIASTRI
  ;

C : F Cs ;

Cs : KASHI Cs
   | /* eps */
   ;

D : OH A UGASS
  | /* eps */
  ;
   

