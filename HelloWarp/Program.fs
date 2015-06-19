open WebSharper
open WebSharper.Html.Server

module Model  =
    type Info = { Name: string; NickName: string}

module Server =
    open Model

    [<Server>]
    let DoWork (s: string) = 
        async {
            return System.String(List.ofSeq s |> List.rev |> Array.ofList)
        }

    [<Server>]
    let Jw(s: string) =
        async { 
            return "Hi Jw, I'm " + s
        }

    [<Server>]
    let Info() =
        async {
            return { Name="Jw"; NickName= "Ploy" }
        }

[<Client>]
module Client =
    open WebSharper.JavaScript
    open WebSharper.Html.Client

    let Main () =
        let input = Input [Attr.Value ""]
        let output = H1 []
        let jw = H1[]
        let info = H1[]
        Div [
            input
            Button([Text "Send"])
                .OnClick (fun _ _ ->
                    async {
                        let! data = Server.DoWork input.Value
                        output.Text <- data
                    } |> Async.Start

                    async {
                        let! data = Server.Jw input.Value
                        jw.Text <- data
                    }|> Async.Start

                    async {
                        let! data = Server.Info()
                        info.Text <- data.Name
                    } |> Async.Start
                )
            HR []
            H4 [Class "text-muted"] -- Text "The server responded:"
            Div [Class "jumbotron"] -< [output]
            Div [] -< [jw]
            Div [] -< [info]
        ]

let MySite =
    Warp.CreateSPA (fun ctx ->
        [
            H1 [Text "Say Hi to the server"]
            Div [ClientSide <@ Client.Main() @>]
        ])

[<EntryPoint>]
do Warp.RunAndWaitForInput(MySite) |> ignore