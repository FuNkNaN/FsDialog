namespace ViewModels

open Models
open FSharp.ViewModule
open System.Collections.ObjectModel

type MainViewModel() as self = 
    inherit ViewModelBase()

    let people = new ObservableCollection<Person>()
    let selectedPerson  = self.Factory.Backing( <@ self.SelectedPerson @>, Person.Empty )    

    do 
        [
            { Name = "Billy"; Email = "B@BobEnterprises.com" }
            { Name = "Karen"; Email = "KMust@ABC.com" }
        ]
        |> List.iter people.Add 

    member __.People = people
    member __.SelectedPerson 
        with get() = selectedPerson.Value 
         and set v = selectedPerson.Value <- v
    
    member __.AddCmd = 
        __.Factory.CommandSync 
            (fun _ -> 
                match Views.DialogHandling.getPerson(Person.Empty) with
                | Some newPerson -> 
                    people.Add newPerson
                | None -> ()
            )

    member __.EditCmd = 
        __.Factory.CommandSync
            (fun _ -> 
                match Views.DialogHandling.getPerson(self.SelectedPerson) with
                | Some editedPerson -> 
                    people.Remove self.SelectedPerson |> ignore
                    people.Add editedPerson
                | None -> ()
            )