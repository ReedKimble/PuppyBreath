Imports PuppyBreath
''' <summary>
''' Represents a collection of game objects keyed by instance id which ignores attempts to add duplicates.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class GameObjectCollection
    Inherits ObjectModel.KeyedCollection(Of Guid, GameObject)

    Protected Overrides Function GetKeyForItem(item As GameObject) As Guid
        Return item.InstanceId
    End Function

    Protected Overrides Sub InsertItem(index As Integer, item As GameObject)
        If Contains(item.InstanceId) Then Return
        MyBase.InsertItem(index, item)
    End Sub

    Protected Overrides Sub SetItem(index As Integer, item As GameObject)
        If Contains(item.InstanceId) Then Return
        MyBase.SetItem(index, item)
    End Sub
End Class
