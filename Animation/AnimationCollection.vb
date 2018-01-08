Namespace Animation
    Public Class AnimationCollection
        Inherits ObjectModel.KeyedCollection(Of String, IAnimation)

        Protected Overrides Function GetKeyForItem(item As IAnimation) As String
            Return item.Name
        End Function
    End Class
End Namespace

