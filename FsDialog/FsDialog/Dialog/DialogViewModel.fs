namespace ViewModels

open Models
open FSharp.ViewModule
open FSharp.ViewModule.Validation

type DialogViewModel (initial: Person) as self = 
    inherit ViewModelBase()    

    let name    = self.Factory.Backing( <@ self.Name @>, initial.Name, hasLengthAtLeast 4 )
    let email   = self.Factory.Backing( <@ self.Email @>, initial.Email, hasLengthAtLeast 5 )

    let makePerson () = { Name = name.Value ; Email = email.Value }

    member __.Name 
        with get() = name.Value 
         and set v = name.Value <- v
    member __.Email 
        with get() = email.Value 
         and set v = email.Value <- v

    member __.Person with get () = makePerson ()