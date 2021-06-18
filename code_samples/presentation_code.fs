// F#
type IntStringUnion =
    | MyInt of int
    | MyString of string

let SayHi input = printfn "%s" ("Hi " + match input with
    | MyInt i -> i.ToString()
    | MyString s -> s
);

let my_integer = MyInt 0;
let my_name = MyString "Aaron";
SayHi(my_integer)
SayHi(my_name)


// F# - type error
let DivideBy(x, y) = x / y;
let y1 = DivideBy(10, 2.0);
let y2 = DivideBy(10, 2);
