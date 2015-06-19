namespace WebSharper.UI.Next.TodoList.Sample
#if INTERACTIVE

#I "bin"
#r "WebSharper.UI.Next.dll"
#endif


open WebSharper

[<JavaScript>]

(* https://github.com/intellifactory/websharper.ui.next/blob/master/docs/Tutorial.md *)

module Jw = 
    open WebSharper.UI.Next
    open WebSharper.UI.Next.Html
    
    type TodoItem = 
        { Done : Var<bool>
          Key : Key
          TodoText : string }
    
    type TodoItem with
        static member Create s = 
            { Key = Key.Fresh()
              TodoText = s
              Done = Var.Create false }
    
    type Model = 
        { Items : ListModel<Key, TodoItem> }
    
    let CreateModel() = { Items = ListModel.Create (fun item -> item.Key) [] }
    
    /// Text node
    let txt t = Doc.TextNode t
    
    /// Input box backed by a variable x
    let input x = Doc.Input x
    
    /// Button with a given caption and handler
    let button name handler = Doc.Button name handler
    
    /// Renders an item.
    let RenderItem coll todo = 
        TR0 [ TD0 [ todo.Done.View
                    |> View.Map(fun isDone -> 
                           if isDone then Del [] [ txt todo.TodoText ]
                           else txt todo.TodoText)
                    |> Doc.EmbedView ] ]
