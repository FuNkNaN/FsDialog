namespace Models

type Person = 
    { 
        Name: string
        Email: string 
    } with 
    static member Empty = { Name = "" ; Email = "" }