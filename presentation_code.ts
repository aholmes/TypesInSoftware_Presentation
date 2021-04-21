// TypeScript
{
    let my_name: string = 'Aaron';
    let my_integer: number = 123;
    let say_hi = (input: number | string): void => console.log(`Hi ${input}`);

    say_hi(my_integer);
    say_hi(my_name);

}

// TypeScript - type error
{
    //let my_name: string = 'Aaron';
    let my_name: string = 123;
    //let my_name: string = undefined;
    function how_many_letters_in_my_name(name: string) {
        let character_count: number = name.length;
        console.log('There are ' + character_count + ' letters in "' + name + '"');
    }
    how_many_letters_in_my_name(my_name);
}