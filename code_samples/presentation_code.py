import sys

## Python - no type hints
my_integer = 0
my_name = 'Aaron'
def say_hi(input):
    print(f'Hi {str(input)}')
say_hi(my_integer)
say_hi(my_name)


## Python - type hints
from typing import Union
my_integer: int = 0
my_name: str = 'Aaron'
def say_hi(input: Union[str,int]) -> None:
    print(f'Hi {str(input)}')
say_hi(my_integer)
say_hi(my_name)


## Python - type error
try:
    my_name = 'Aaron'
    #my_name = 0
    #my_name = None
    def how_many_letters_in_my_name(name: str):
        character_count: int = len(name)
        print('There are ' + character_count + ' letters in "' + name + '"')
    how_many_letters_in_my_name(my_name)
except TypeError:
    print(sys.exc_info())


## PYthon - class and object
class Foo:
    def __init__(self, bar):
        self.bar = bar
print(type(Foo))

foo = Foo('Hello!')
print(foo)
print(type(foo))
print(vars(foo)) 
