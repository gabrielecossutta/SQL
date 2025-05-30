#Region "INTERACE"
''' <summary>
''' Interface for CRUD operations
''' </summary>
Public Interface ICRUD

    Function Create(Of T)(ParamArray args() As Object) As T

    Function Read(Of T)(ParamArray args() As Object) As T

    Function Update(Of T)(ParamArray args() As Object) As T

    Function Delete(Of T)(ParamArray args() As Object) As T

End Interface

#End Region

#Region "BASE CLASS"
''' <summary>
''' Base class that implements the ICRUD interface, cant be instantiated directly.
''' It provides default implementations for Create, Read, Update, and Delete methods.
''' Derived classes can override these methods to provide specific functionality.
''' </summary>
Public MustInherit Class BaseTable

    'Impleements ICRUD interface
    Implements ICRUD

    ''' <summary>
    ''' Implements the Create method from the ICRUD interface.
    ''' This method can be overridden in derived classes to provide specific functionality.
    ''' </summary>
    ''' <typeparam name="T">The type of the object to be created.</typeparam>
    ''' <param name="args">Variable number of arguments.</param>
    ''' <returns>Returns an object of type T, default is Nothing.</returns>
    Public Overridable Function Create(Of T)(ParamArray args() As Object) As T Implements ICRUD.Create

        For Each arg As Object In args

            Console.WriteLine($"Create: {arg.ToString()} Value: {arg}")

        Next

        Console.WriteLine()

        Return Nothing

    End Function

    ''' <summary>
    ''' Implements the Read method from the ICRUD interface.
    ''' This method can be overridden in derived classes to provide specific functionality.
    ''' </summary>
    ''' <typeparam name="T">The type of the object to be created.</typeparam>
    ''' <param name="args">Variable number of arguments.</param>
    ''' <returns>Returns an object of type T, default is Nothing.</returns>
    Public Function Read(Of T)(ParamArray args() As Object) As T Implements ICRUD.Read

        For Each arg As Object In args

            Console.WriteLine($"Read: {arg.GetType.ToString()} Value: {arg}")

        Next

        Console.WriteLine()

        Return Nothing

    End Function

    ''' <summary>
    ''' Implements the Update method from the ICRUD interface.
    ''' This method can be overridden in derived classes to provide specific functionality.
    ''' </summary>
    ''' <typeparam name="T">The type of the object to be created.</typeparam>
    ''' <param name="args">Variable number of arguments.</param>
    ''' <returns>Returns an object of type T, default is Nothing.</returns>
    Public Overridable Function Update(Of T)(ParamArray args() As Object) As T Implements ICRUD.Update

        For Each arg As Object In args

            Console.WriteLine($"Update: {arg.GetType.ToString()} Value: {arg}")

        Next

        Console.WriteLine()

        Return Nothing

    End Function

    ''' <summary>
    ''' Implements the Delete method from the ICRUD interface.
    ''' This method can be overridden in derived classes to provide specific functionality.
    ''' </summary>
    ''' <typeparam name="T">The type of the object to be created.</typeparam>
    ''' <param name="args">Variable number of arguments.</param>
    ''' <returns>Returns an object of type T, default is Nothing.</returns>
    Public Function Delete(Of T)(ParamArray args() As Object) As T Implements ICRUD.Delete

        For Each arg As Object In args

            Console.WriteLine($"Delete: {arg.GetType.ToString()} Value: {arg}")

        Next

        Console.WriteLine()

        Return Nothing

    End Function

End Class

#End Region

#Region "CLASS"
Public Class Table1

    'inherits from basetable
    Inherits BaseTable

    'Override the Create method to provide specific functionality
    Public Overrides Function Create(Of T)(ParamArray args() As Object) As T

        For Each arg As Object In args

            Console.WriteLine($"-----Override-----Create Value: {arg}")

        Next

        ' Call the base class Create method
        MyBase.Create(Of T)(args)

        Console.WriteLine()

        Return Nothing

    End Function

    Public Overrides Function Create(Of T)(ParamArray args() As Object) As T

        Return MyBase.Create(Of T)(args)

    End Function

End Class

Public Class Table2

    Inherits BaseTable

    ''Override the Update method to provide specific functionality
    Public Overrides Function Update(Of T)(ParamArray args() As Object) As T

        For Each arg As Object In args

            Console.WriteLine($"-----Override-----Update Value: {arg}")

        Next

        Console.WriteLine()

        Return Nothing

    End Function

End Class

#End Region

Module InterfaceCRUD

#Region "MAIN"
    Sub Main()

        Dim returnValue As Integer

        ' Create instances of Table1 and Table2
        Dim table1 As New Table1()
        Dim table2 As New Table2()

        'Test the CRUD operations, the override and the number of parameters
        returnValue = table1.Create(Of String)("Matteo", 1, 4D)
        returnValue = table1.Update(Of String)(1, "Matteo", 1, 4D)
        returnValue = table1.Read(Of String)("Matteo", 1.0F, 4D)
        returnValue = table1.Delete(Of String)("Matteo", 1D, 4D)
        Console.WriteLine("________________________________________")
        returnValue = table2.Create(Of Integer)(1, 3)
        returnValue = table2.Update(Of Integer)("Matteo")
        returnValue = table2.Read(Of Integer)(1.0F, 2D, 3)
        returnValue = table2.Delete(Of Integer)(1, 2, 3, 4, 5, 6, 7, 8, 9)


        'Dim table1 As New Table1("oggetto connessione")
        'table1.property1 = "cose"
        'table1.property2 = "cose"
        'table1.insert


    End Sub

#End Region

End Module

