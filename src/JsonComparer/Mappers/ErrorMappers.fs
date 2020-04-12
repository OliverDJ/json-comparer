module ErrorMappers

open ErrorModels

let fillErrorMsg p a b eq =
    {
        Path = p 
        Value = a 
        Found = b 
        Equal = eq
    }

let mapErrorMsgBoolToErrorMsg (a:ErrorMsgBool) =
    {
        Path = a.Path
        Value = a.Value
        Found = a.Found
    }


let mapToDistinctError (x: ErrorMsg) =
    {
        Path = x.Path
        Value = x.Value
    }

let mapToSymmetricalError (x: ErrorMsg) (y: ErrorMsg) =
    {
        Path = x.Path
        ValueA = x.Value
        ValueB = y.Value
    }

let createErrors nA nB sym dA dB =
    {
        FileA = nA
        FileB = nB
        SymmetricalErrors = sym
        DistinctErrorsA = dA
        DistinctErrorsB = dB
    }
