﻿module FsUnit

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open NHamcrest
open NHamcrest.Core

type Assert with
    static member That<'a> (actual, matcher:IMatcher<'a>) =
        if not (matcher.Matches(actual)) then
            let description = new StringDescription()
            matcher.DescribeTo(description)
            let mismatchDescription = new StringDescription()
            matcher.DescribeMismatch(actual, mismatchDescription)
            raise (new AssertFailedException(sprintf "%s %s" (description.ToString()) (mismatchDescription.ToString())))

let inline should (f : 'a -> ^b) x (y : obj) =
    let c = f x
    let y =
        match y with
        | :? (unit -> unit) as assertFunc -> box assertFunc
        | _ -> y
    Assert.That(y, c)

let equal expected = CustomMatchers.equal expected

let equalWithin (tolerance:obj) (expected:obj) = CustomMatchers.equalWithin tolerance expected

let not' (expected:obj) = CustomMatchers.not' expected

let throw (t:Type) = CustomMatchers.throw t

let be = CustomMatchers.be

let Null = CustomMatchers.Null

let EmptyString = CustomMatchers.EmptyString

let NullOrEmptyString = CustomMatchers.NullOrEmptyString

let True = CustomMatchers.True

let False = CustomMatchers.False

let sameAs expected = CustomMatchers.sameAs expected

let greaterThan (expected:obj) = CustomMatchers.greaterThan expected

let greaterThanOrEqualTo (expected:obj) = CustomMatchers.greaterThanOrEqualTo expected

let lessThan (expected:obj) = CustomMatchers.lessThan expected

let lessThanOrEqualTo (expected:obj) = CustomMatchers.lessThanOrEqualTo expected

let endWith (expected:string) = CustomMatchers.endWith expected

let startWith (expected:string) = CustomMatchers.startWith expected

let ofExactType<'a> = CustomMatchers.ofExactType<'a>

let contain expected = CustomMatchers.contain expected

module FsUnitDepricated =
    let not x = not' x 

// haveLength, haveCount, Empty, and shouldFail are not implemented for MbUnit, xUnit, or MsTest 