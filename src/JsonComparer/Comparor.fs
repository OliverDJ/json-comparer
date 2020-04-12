namespace JsonComparer

    module Comparor =
        open Newtonsoft.Json.Linq
        open System
        open ErrorModels
        open ErrorMappers

        let compareValue (t: string->string) (x:string) (y: string) =
            let xtrans = x |> t
            let ytrans = y |> t
            xtrans = ytrans

        let private getValue (tree: JObject) path = 
            let r = path |> tree.SelectToken
            let guid = Guid.NewGuid().ToString()
            let ret = if r = null then guid else r.ToString()
            ret
    
        let private _comparePath treeA treeB p =
            let aVal = p |> getValue treeA
            let bVal = p |> getValue treeB
            let eq = aVal = bVal
            let ret = fillErrorMsg p aVal bVal eq
            ret

        let removeTrue s = s |> Seq.filter (fun em -> not em.Equal ) |> Seq.map mapErrorMsgBoolToErrorMsg

        let comparePath = _comparePath

