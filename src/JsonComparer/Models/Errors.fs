module ErrorModels


// Before Combiner
type ErrorMsgBool = 
    {
        Path : string
        Value: string
        Found: string
        Equal: bool 
    }

type ErrorMsg = 
    {
        Path : string
        Value: string
        Found: string
    }

// During Combiner
type SymmetricalError = 
    {
        Path: string
        ValueA: string
        ValueB: string
    }

type DistinctError =
    {
        Path: string
        Value: string
    }

type ErrorType =
| DistinctErrorType of DistinctError
| SymmetricalErrorType of SymmetricalError

type Errors = 
    {
        FileA: string
        FileB: string
        SymmetricalErrors: SymmetricalError list 
        DistinctErrorsA: DistinctError list 
        DistinctErrorsB: DistinctError list 
    }


  


