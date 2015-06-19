namespace WebSharper.UI.Next.TodoList.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next

[<JavaScript>]
module Code = 
    [<Literal>]
    let template = __SOURCE_DIRECTORY__ + "/index.html"
    
    type IndexTemplate = Templating.Template<template>
    
    [<NoComparison>]
    type Task = 
        { Name : string
          Done : Var<bool> }
    
    let Tasks = 
        ListModel.Create (fun task -> task.Name) [ { Name = "Have breakfast"
                                                     Done = Var.Create true }
                                                   { Name = "Have lunch"
                                                     Done = Var.Create false }
                                                   { Name = "Have lunch"
                                                     Done = Var.Create false } ]
    
    let NewTaskName = Var.Create ""
    
    let AddHandler = 
        fun _ -> 
            Tasks.Add { Name = NewTaskName.Value
                        Done = Var.Create false }
            Var.Set NewTaskName ""
    
    let ClearHandler = fun _ -> Tasks.RemoveBy(fun task -> task.Done.Value)
    let ListHandler = 
        (ListModel.View Tasks 
         |> Doc.Convert
                (fun task -> 
                IndexTemplate.ListItem.Doc
                    (Task = View.Const task.Name, Clear = (fun _ -> Tasks.RemoveByKey task.Name), Done = task.Done, 
                     ShowDone = Attr.DynamicClass "checked" task.Done.View id)))

    let Main = 
        IndexTemplate.Main.Doc
            (ListContainer = ListHandler, NewTaskName = NewTaskName, Add = AddHandler, ClearCompleted = ClearHandler) 
        |> Doc.RunById "tasks"
