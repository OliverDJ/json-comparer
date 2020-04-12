namespace JsonComparer

    
    module Json =
        open Newtonsoft.Json.Linq
        open Collector
        open Comparor
        open ErrorModels
        
        let private _scanPathAndCompare f = loop >> (Seq.map f) >> removeTrue
        
        let compareJsonObject nA nB (treeA: JObject) (treeB: JObject)  =
            let comparerA = comparePath treeA treeB
            let comparerB = comparePath treeB treeA
            let (left: ErrorMsg list) = (comparerA, treeA.Root) ||> _scanPathAndCompare |> Seq.toList
            let (right: ErrorMsg list) = (comparerB, treeB.Root) ||> _scanPathAndCompare |> Seq.toList
            let errors = (left, right) ||> (Combiner.combine nA nB)
            errors

        let compareJsonNamed nameA nameB (jsonA: string)(jsonB: string) =
            let treeA = jsonA |> JObject.Parse
            let treeB = jsonB |> JObject.Parse
            compareJsonObject nameA nameB treeA treeB
            
        let compareJson = compareJsonNamed "left" "right"
        
        
    
