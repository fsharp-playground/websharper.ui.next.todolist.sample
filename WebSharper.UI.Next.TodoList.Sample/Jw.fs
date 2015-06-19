namespace WebSharper.UI.Next.TodoList.Sample
#if INTERACTIVE

#I "bin"
#r "WebSharper.UI.Next.dll"
#endif


open WebSharper

[<JavaScript>]

(* https://github.com/intellifactory/websharper.ui.next/blob/master/docs/Tutorial.md *)
(* New: http://websharper.com/docs/ui.next *)

module Jw = 
    open WebSharper.UI.Next
    open WebSharper.UI.Next.Html
   
    type LoginData = { Username: string; Password: string }

    let Hello = 
        let rvContent = Var.Create ""
        let vUpperContent =
            rvContent.View
            |> View.Map (fun t -> t.ToUpper())

        Div [] [
            Doc.Input [] rvContent
            Label [] [Doc.TextView vUpperContent]
        ]

    let JwView =
        let rvContent = Var.Create "initial value"
        let vContent = rvContent.View
        // vContent's current value is now "initial value"
        rvContent.Value <- "new value"

    let Login = 
        let rvSubmit = Var.Create ()
        let rvUsername = Var.Create ""
        let rvPassword = Var.Create ""
        ()

        (*
        let vLoginResult =
            View.Const (fun u p -> Some { Username = u; Password = p })
            <*> rvUsername.View
            <*> rvPassword.View
            |> View.SnapshotOn () rvSubmit.View
            |> View.MapAsync (function
                | None ->
                    async { return () }
                | Some loginData ->
                    Rpc.LoginUser loginData)

        Div [] [
            Doc.Input [] rvUsername
            Doc.PasswordBox [] rvPassword
            Doc.Button "Log in" [] (Var.Set rvSubmit)
            vLoginResult |> View.Map (fun () -> Doc.Empty) |> Doc.EmbedView
        ]
        *)