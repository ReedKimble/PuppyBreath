''' <summary>
''' Represents name-keyed collections of flag, number and text variables.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
<Serializable>
Public Class VariableBank
    Private flags As New Dictionary(Of String, Boolean)
    Private numbers As New Dictionary(Of String, Single)
    Private objects As New Dictionary(Of String, GameObject)
    Private texts As New Dictionary(Of String, String)

    ''' <summary>
    ''' Gets or sets a flag value.
    ''' </summary>
    ''' <param name="name">The name of the variable.</param>
    ''' <returns>True if the flag is set, otherwise false.</returns>
    Public Property Flag(name As String) As Boolean
        Get
            Return GetFlag(name)
        End Get
        Set(value As Boolean)
            SetFlag(name, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a number value.
    ''' </summary>
    ''' <param name="name">The name of the variable.</param>
    ''' <returns>The numeric value of the variable.</returns>
    Public Property Number(name As String) As Single
        Get
            Return GetNumber(name)
        End Get
        Set(value As Single)
            SetNumber(name, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a number value.
    ''' </summary>
    ''' <param name="name">The name of the variable.</param>
    ''' <returns>The numeric value of the variable.</returns>
    Public Property [Object](name As String) As GameObject
        Get
            Return GetObject(name)
        End Get
        Set(value As GameObject)
            SetObject(name, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a text value.
    ''' </summary>
    ''' <param name="name">The name of the variable.</param>
    ''' <returns>The text value of the variable.</returns>
    Public Property Text(name As String) As String
        Get
            Return GetText(name)
        End Get
        Set(value As String)
            SetText(name, value)
        End Set
    End Property

    ''' <summary>
    ''' Clears all variables and values.
    ''' </summary>
    Public Sub Clear()
        flags.Clear()
        numbers.Clear()
        objects.Clear()
        texts.Clear()
    End Sub

    ''' <summary>
    ''' Copies all values from this bank into the other bank.
    ''' </summary>
    ''' <param name="other">The variable bank to copy into.</param>
    Public Sub CopyInto(other As VariableBank)
        For Each key In flags.Keys.ToArray
            other.flags(key) = flags(key)
        Next
        For Each key In numbers.Keys.ToArray
            other.numbers(key) = numbers(key)
        Next
        For Each key In objects.Keys.ToArray
            other.objects(key) = objects(key)
        Next
        For Each key In texts.Keys.ToArray
            other.texts(key) = texts(key)
        Next
    End Sub

    ''' <summary>
    ''' Checks to see if the named text variable contains the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The variable to check.</param>
    ''' <param name="valueToCompare">The value to compare against.</param>
    ''' <returns>True if the variable contains the value, otherwise false.</returns>
    Public Function DoesTextContain(nameofVariable As String, valueToCompare As String) As Boolean
        Return GetText(nameofVariable).Contains(valueToCompare)
    End Function

    ''' <summary>
    ''' Checks to see if the named text variable starts with the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The variable to check.</param>
    ''' <param name="valueToCompare">The value to compare against.</param>
    ''' <returns>True if the variable starts with the value, otherwise false.</returns>
    Public Function DoesTextStartWith(nameofVariable As String, valueToCompare As String) As Boolean
        Return GetText(nameofVariable).StartsWith(valueToCompare)
    End Function

    ''' <summary>
    ''' Checks to see if the named text variable ends with the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The variable to check.</param>
    ''' <param name="valueToCompare">The value to compare against.</param>
    ''' <returns>True if the variable ends with the value, otherwise false.</returns>
    Public Function DoesTextEndWith(nameofVariable As String, valueToCompare As String) As Boolean
        Return GetText(nameofVariable).EndsWith(valueToCompare)
    End Function

    ''' <summary>
    ''' Gets a value indicating if a flag is set.
    ''' </summary>
    ''' <param name="name">The name of the variable to check.</param>
    ''' <returns>True if the flag is set, otherwise false.</returns>
    Public Function GetFlag(name As String) As Boolean
        NullNameCheck(name)
        If Not flags.ContainsKey(name) Then Return False
        Return flags(name)
    End Function

    ''' <summary>
    ''' Gets the value of a numeric variable.
    ''' </summary>
    ''' <param name="name">The name of the variable to get.</param>
    ''' <returns>The value of the variable.</returns>
    Public Function GetNumber(name As String) As Single
        NullNameCheck(name)
        If Not numbers.ContainsKey(name) Then Return 0!
        Return numbers(name)
    End Function

    ''' <summary>
    ''' Gets the value of a GameObject variable.
    ''' </summary>
    ''' <param name="name">The name of the variable to get.</param>
    ''' <returns>The value of the variable.</returns>
    Public Function GetObject(name As String) As GameObject
        NullNameCheck(name)
        If Not objects.ContainsKey(name) Then Return Nothing
        Return objects(name)
    End Function

    ''' <summary>
    ''' Gets the value of a text variable.
    ''' </summary>
    ''' <param name="name">The name of the variable to get.</param>
    ''' <returns>The value of the variable.</returns>
    Public Function GetText(name As String) As String
        NullNameCheck(name)
        If Not texts.ContainsKey(name) Then Return String.Empty
        Return texts(name)
    End Function

    ''' <summary>
    ''' Gets a value indicating if the variable exists.
    ''' </summary>
    ''' <param name="name">The name of the variable to check.</param>
    ''' <returns>True if the variable exists, otherwise false.</returns>
    Public Function HasFlag(name As String) As Boolean
        NullNameCheck(name)
        Return flags.ContainsKey(name)
    End Function

    ''' <summary>
    ''' Gets a value indicating if the variable exists.
    ''' </summary>
    ''' <param name="name">The name of the variable to check.</param>
    ''' <returns>True if the variable exists, otherwise false.</returns>
    Public Function HasNumber(name As String) As Boolean
        NullNameCheck(name)
        Return numbers.ContainsKey(name)
    End Function

    ''' <summary>
    ''' Gets a value indicating if the variable exists.
    ''' </summary>
    ''' <param name="name">The name of the variable to check.</param>
    ''' <returns>True if the variable exists, otherwise false.</returns>
    Public Function HasObject(name As String) As Boolean
        NullNameCheck(name)
        Return objects.ContainsKey(name)
    End Function

    ''' <summary>
    ''' Gets a value indicating if the variable exists.
    ''' </summary>
    ''' <param name="name">The name of the variable to check.</param>
    ''' <returns>True if the variable exists, otherwise false.</returns>
    Public Function HasText(name As String) As Boolean
        NullNameCheck(name)
        Return texts.ContainsKey(name)
    End Function

    ''' <summary>
    ''' Sets the value of a variable.
    ''' </summary>
    ''' <param name="name">The name of the variable to set.</param>
    ''' <param name="value">The value to set the variable true.</param>
    ''' <returns>True if the variable is new or its value has changed, otherwise false.</returns>
    Public Function SetFlag(name As String, value As Boolean) As Boolean
        NullNameCheck(name)
        Dim changed = If(Not flags.ContainsKey(name), True, flags(name) <> value)
        flags(name) = value
        Return changed
    End Function

    ''' <summary>
    ''' Sets the value of a variable.
    ''' </summary>
    ''' <param name="name">The name of the variable to set.</param>
    ''' <param name="value">The value to set the variable true.</param>
    ''' <returns>True if the variable is new or its value has changed, otherwise false.</returns>
    Public Function SetNumber(name As String, value As Single) As Boolean
        NullNameCheck(name)
        Dim changed = If(Not numbers.ContainsKey(name), True, numbers(name) <> value)
        numbers(name) = value
        Return changed
    End Function

    ''' <summary>
    ''' Sets the value of a variable.
    ''' </summary>
    ''' <param name="name">The name of the variable to set.</param>
    ''' <param name="value">The value to set the variable true.</param>
    ''' <returns>True if the variable is new or its value has changed, otherwise false.</returns>
    Public Function SetObject(name As String, value As GameObject) As Boolean
        NullNameCheck(name)
        Dim changed = If(Not objects.ContainsKey(name), True, objects(name) IsNot value)
        objects(name) = value
        Return changed
    End Function

    ''' <summary>
    ''' Sets the value of a variable.
    ''' </summary>
    ''' <param name="name">The name of the variable to set.</param>
    ''' <param name="value">The value to set the variable true.</param>
    ''' <returns>True if the variable is new or its value has changed, otherwise false.</returns>
    Public Function SetText(name As String, value As String) As Boolean
        NullNameCheck(name)
        If value Is Nothing Then value = String.Empty
        Dim changed = If(Not texts.ContainsKey(name), True, texts(name) <> value)
        texts(name) = value
        Return changed
    End Function

    ''' <summary>
    ''' Inserts all values from this bank into the other if they do not exist.
    ''' </summary>
    ''' <param name="other">The variable bank to insert into.</param>
    Public Sub InsertInto(other As VariableBank)
        For Each key In flags.Keys.ToArray
            If Not other.flags.ContainsKey(key) Then other.flags(key) = flags(key)
        Next
        For Each key In numbers.Keys.ToArray
            If Not other.numbers.ContainsKey(key) Then other.numbers(key) = numbers(key)
        Next
        For Each key In objects.Keys.ToArray
            If Not other.objects.ContainsKey(key) Then other.objects(key) = objects(key)
        Next
        For Each key In texts.Keys.ToArray
            If Not other.texts.ContainsKey(key) Then other.texts(key) = texts(key)
        Next
    End Sub

    ''' <summary>
    ''' Checks to see if a variable has the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the variable to check.</param>
    ''' <param name="valueToCompare">The value to compare the variable against.</param>
    ''' <returns>True if the comparision is true, otherwise false.</returns>
    Public Function IsFlag(nameofVariable As String, valueToCompare As Boolean) As Boolean
        Return GetFlag(nameofVariable) = valueToCompare
    End Function

    ''' <summary>
    ''' Checks to see if the flag is set.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the flag variable to check.</param>
    ''' <returns>True if the flag is set, otherwise false.</returns>
    Public Function IsFlagSet(nameofVariable As String) As Boolean
        Return GetFlag(nameofVariable) = True
    End Function

    ''' <summary>
    ''' Checks to see if a variable has the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the variable to check.</param>
    ''' <param name="valueToCompare">The value to compare the variable against.</param>
    ''' <returns>True if the comparision is true, otherwise false.</returns>
    Public Function IsNumber(nameofVariable As String, valueToCompare As Single) As Boolean
        Return GetNumber(nameofVariable) = valueToCompare
    End Function

    ''' <summary>
    ''' Checks to see if a variable has the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the variable to check.</param>
    ''' <param name="valueToCompare">The value to compare the variable against.</param>
    ''' <returns>True if the comparision is true, otherwise false.</returns>
    Public Function IsNumberNot(nameofVariable As String, valueToCompare As Single) As Boolean
        Return Not GetNumber(nameofVariable) = valueToCompare
    End Function

    ''' <summary>
    ''' Checks to see if a variable has the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the variable to check.</param>
    ''' <param name="valueToCompare">The value to compare the variable against.</param>
    ''' <returns>True if the comparision is true, otherwise false.</returns>
    Public Function IsNumberGreaterThan(nameofVariable As String, valueToCompare As Single) As Boolean
        Return GetNumber(nameofVariable) > valueToCompare
    End Function

    ''' <summary>
    ''' Checks to see if a variable has the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the variable to check.</param>
    ''' <param name="valueToCompare">The value to compare the variable against.</param>
    ''' <returns>True if the comparision is true, otherwise false.</returns>
    Public Function IsNumberLessThan(nameofVariable As String, valueToCompare As Single) As Boolean
        Return GetNumber(nameofVariable) < valueToCompare
    End Function

    ''' <summary>
    ''' Checks to see if a variable has the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the variable to check.</param>
    ''' <param name="valueToCompare">The value to compare the variable against.</param>
    ''' <returns>True if the comparision is true, otherwise false.</returns>
    Public Function IsNumberGreaterThanEqual(nameofVariable As String, valueToCompare As Single) As Boolean
        Return GetNumber(nameofVariable) >= valueToCompare
    End Function

    ''' <summary>
    ''' Checks to see if a variable has the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the variable to check.</param>
    ''' <param name="valueToCompare">The value to compare the variable against.</param>
    ''' <returns>True if the comparision is true, otherwise false.</returns>
    Public Function IsNumberLessThanEqual(nameofVariable As String, valueToCompare As Single) As Boolean
        Return GetNumber(nameofVariable) <= valueToCompare
    End Function

    ''' <summary>
    ''' Checks to see if a variable has the specified value.
    ''' </summary>
    ''' <param name="nameofVariable">The name of the variable to check.</param>
    ''' <param name="valueToCompare">The value to compare the variable against.</param>
    ''' <returns>True if the comparision is true, otherwise false.</returns>
    Public Function IsText(nameofVariable As String, valueToCompare As String) As Boolean
        Return GetText(nameofVariable) = valueToCompare
    End Function

    Private Sub NullNameCheck(name As String)
        If String.IsNullOrEmpty(name) Then Throw New ArgumentException("Name cannot be null or empty.", "name")
    End Sub

    ''' <summary>
    ''' Synchronizes all values from this bank into the other, removing values that do not exist in this bank.
    ''' </summary>
    ''' <param name="other">The variable bank to sync into.</param>
    Public Sub SyncInto(other As VariableBank)
        Dim originalKeys() As String = other.flags.Keys.ToArray
        For Each key In flags.Keys.ToArray
            other.flags(key) = flags(key)
        Next
        For Each key In originalKeys
            If Not flags.ContainsKey(key) Then other.flags.Remove(key)
        Next

        originalKeys = other.numbers.Keys.ToArray
        For Each key In numbers.Keys.ToArray
            other.numbers(key) = numbers(key)
        Next
        For Each key In originalKeys
            If Not numbers.ContainsKey(key) Then other.numbers.Remove(key)
        Next

        originalKeys = other.objects.Keys.ToArray
        For Each key In objects.Keys.ToArray
            other.objects(key) = objects(key)
        Next
        For Each key In originalKeys
            If Not objects.ContainsKey(key) Then other.objects.Remove(key)
        Next

        originalKeys = other.texts.Keys.ToArray
        For Each key In texts.Keys.ToArray
            other.texts(key) = texts(key)
        Next
        For Each key In originalKeys
            If Not texts.ContainsKey(key) Then other.texts.Remove(key)
        Next
    End Sub

    ''' <summary>
    ''' Updates all values in the other bank with the values from this bank, if they exist in the other bank.
    ''' </summary>
    ''' <param name="other">The other variable bank to update.</param>
    Public Sub UpdateInto(other As VariableBank)
        For Each key In flags.Keys.ToArray
            If other.flags.ContainsKey(key) Then other.flags(key) = flags(key)
        Next
        For Each key In numbers.Keys.ToArray
            If other.numbers.ContainsKey(key) Then other.numbers(key) = numbers(key)
        Next
        For Each key In objects.Keys.ToArray
            If other.objects.ContainsKey(key) Then other.objects(key) = objects(key)
        Next
        For Each key In texts.Keys.ToArray
            If other.texts.ContainsKey(key) Then other.texts(key) = texts(key)
        Next
    End Sub
End Class
