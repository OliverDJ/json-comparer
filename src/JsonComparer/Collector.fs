namespace JsonComparer
    

    module Collector =
        open Newtonsoft.Json.Linq

        let private _hasChildren (jt: JToken) = jt.HasValues
        
        let private _handleLeaf(l:JToken) = [l.Path]
    
        let rec private _loop (r: JToken) =
            let handleNode (c: JToken) =
                let isLeaf = c |> _hasChildren |> not
                match isLeaf with
                | true -> c |> _handleLeaf
                | false -> c |> _loop

            let ret = 
                r.Children()
                |> Seq.map handleNode
                |> Seq.collect id
                |> Seq.toList
            ret
    
        let loop = _loop
    