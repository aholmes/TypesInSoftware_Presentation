// JavaScript
{
    let my_name = 'Aaron';
    let my_integer = 123;
    let say_hi = input => console.log(`Hi ${input}`);

    say_hi(my_integer);
    say_hi(my_name);
}

{
    // JavaScript - type error
    //var my_name = 'Aaron';
    //var my_name = 123;
    var my_name = undefined;
    function how_many_letters_in_my_name(name) {
        var character_count = name.length;
        console.log('There are ' + character_count + ' letters in "' + name + '"');
    }
    how_many_letters_in_my_name(my_name);
}