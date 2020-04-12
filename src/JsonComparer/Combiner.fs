namespace JsonComparer

    module Combiner =

        open ErrorModels
        open ErrorMappers

        let private _areSymmetrical (a:ErrorMsg) (b:ErrorMsg) =
            let r = (a.Path = b.Path) && (a.Found = b.Value) && (b.Found = a.Value)
            r

        let private _getDistinct acc e =
            match e with
            |DistinctErrorType s -> List.append acc [s]
            | _ -> acc

        let private _getSymmetrical acc e =
            match e with
            |SymmetricalErrorType s -> List.append acc [s]
            | _ -> acc

        let private _extract folder (s: ErrorType list) =
            let r = s |> List.fold folder []
            r

        let private _handleDistinctError (x: ErrorMsg) = x |> mapToDistinctError
    
        let private _handleSymmetricalError (x: ErrorMsg) (y: ErrorMsg) =
            (x, y) ||> mapToSymmetricalError
    

        let private _mapToErrorTypes (tup) =
            match tup with
            | (x , None) -> x |> _handleDistinctError |> DistinctErrorType
            | (x , Some y) -> (x, y) ||> _handleSymmetricalError |> SymmetricalErrorType


        let private _combineOneSide f (a:ErrorMsg list) (b:ErrorMsg list) =  
            let r = a |> List.map (fun x -> (x, List.tryFind (f x) b))
            let ret = r |> List.map _mapToErrorTypes
            ret 

        let private _symCombine = _combineOneSide _areSymmetrical

        let private _combine nA nB errLiA errLiB =
            let left = (errLiA, errLiB) ||> _symCombine
            let right = (errLiB, errLiA) ||> _symCombine
            let distinctA = left |> (_extract _getDistinct)
            let distinctB = right |> (_extract _getDistinct)
            let symmetrical = left |> (_extract _getSymmetrical)
            let errors = createErrors nA nB symmetrical distinctA distinctB 
            errors

        let combine = _combine