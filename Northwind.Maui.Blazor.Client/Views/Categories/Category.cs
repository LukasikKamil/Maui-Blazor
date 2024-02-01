﻿// ObservableObject, [ObservableProperty]
using CommunityToolkit.Mvvm.ComponentModel;

namespace Northwind.Maui.Blazor.Client.Views.Categories;

internal partial class Category : ObservableObject
{
    // The field must by camelCase because the source-generated
    // public property names will be TitleCase

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PicturePath))]
    private int categoryId;

    [ObservableProperty]
    private string categoryName;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private byte[] picture;

    [ObservableProperty]
    private string picturePath;
}
