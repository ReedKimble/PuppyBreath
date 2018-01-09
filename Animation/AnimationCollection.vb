Namespace Animation
    ''' <summary>
    ''' Represents a named collection of animations.
    ''' </summary>
    ''' <author>Reed Kimble 01/08/2018</author>
    Public Class AnimationCollection
        Inherits ObjectModel.KeyedCollection(Of String, IAnimation)

        Protected Overrides Function GetKeyForItem(item As IAnimation) As String
            Return item.Name
        End Function
    End Class
End Namespace

