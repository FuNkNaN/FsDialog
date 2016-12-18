namespace Views

open FsXaml

type PersonDialogBase = XAML<"Dialog/PersonDialog.xaml">

type PersonDialog() as dlg =
    inherit PersonDialogBase()

    do dlg.OkBtn.Click.Add( fun _ -> dlg.DialogResult <- System.Nullable(true) )

module DialogHandling =

    let getPerson initial =
        let dlg = PersonDialog(DataContext = ViewModels.DialogViewModel(initial))
        if dlg.ShowDialog() = System.Nullable(true) then
            let person = 
                match dlg.DataContext with
                | :? ViewModels.DialogViewModel as dialogVM 
                    -> dialogVM.Person
                | _ -> failwith "Not a DialogViewModel"
            Some person
        else None