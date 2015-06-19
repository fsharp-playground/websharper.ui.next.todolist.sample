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
                                                   { Name = "Play football"
                                                     Done = Var.Create false } ]
    
    let NewTaskName = Var.Create ""
    
    let AddHandler = 
        fun _ -> 
            Tasks.Add { Name = NewTaskName.Value
                        Done = Var.Create false }
            Var.Set NewTaskName ""
    
    let Remove task = fun _ -> Tasks.RemoveByKey task.Name
    let ShowDone task = Attr.DynamicClass "checked" task.Done.View id
    let Convert = 
        fun task -> 
            IndexTemplate.ListItem.Doc
                (Task = View.Const task.Name, Clear = Remove task, Done = task.Done, ShowDone = ShowDone task)
    let ClearHandler = fun _ -> Tasks.RemoveBy(fun task -> task.Done.Value)
    let ListInfo = (ListModel.View Tasks |> Doc.Convert Convert)
    let Main = 
        IndexTemplate.Main.Doc
            (ListContainer = ListInfo, NewTaskName = NewTaskName, Add = AddHandler, ClearCompleted = ClearHandler) 
        |> Doc.RunById "tasks"