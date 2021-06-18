# R
my_integer <- as.integer(0)
my_name <- "Aaron"

say_hi <- function(input)
    print(paste("Hi", input))

say_hi(my_integer)
say_hi(my_name)

# R - type error
#my_name <- "Aaron"
my_name <- as.integer(123)
#my_name <- NA
how_many_letters_in_my_name <- function(name) {
    character_count <- nchar(name)
    print(paste("There are", character_count, "letters in '", name, "'"))
}
how_many_letters_in_my_name(my_name)