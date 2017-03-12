//======================================================================================================
// NamingConvention.cs
// Description: Basic naming conventions programmers should follow during the development process.
// Should be revised througout development.
// Author: Reynald Brassard
//======================================================================================================

//======================================================================================================
// Classes: 
//
// CamelCase with first letter capitalized. Braces begin on next line after class
// declaration
//
// example:
// class Foo
// {
//
// }
//======================================================================================================

//======================================================================================================
// Methods (or functions):
//
// CamelCase with first letter capitalized. Braces begin on next line after function
// declaration. Parameters camelCase with first letter lowercase.
// 
// example:
// void Foo(int bar, float fooBar)
// {
//      
// }
//======================================================================================================

//======================================================================================================
// Properties: 
//
// CamelCase with first letter capitalized. Braces begin on next line after property
// declaration.
//
// example:
// public int Foo
// {
//      get{ return bar; }
//      set{ fooBar = value; }
// }
//======================================================================================================

//======================================================================================================
// MemberVariables: 
//
// camelCase with first letter lowercase.
//
// example:
// int foo;
// float fooBar;
//======================================================================================================

//======================================================================================================
// General Guidlines: 
//
// Sections inside classes which are logically different should be seperated
// by comments, for easier readability. #regions are recommended to aide with the seperation of
// key areas.
//
// Member variables should not be set as public unless other classes are required to access their data
// Prefer setting [Serializable] attribute.
// example:
// [Serializable]
// int maxSize;
//
// preferred over
//
// public int maxSize;
//
// Member variables should be seperated into sections by their use, and those sections (if serializable)
// should be seperated by the [Header] attribute.
// example:
// [Header("Attacking")]
// [Serializable]
// float attackSpeed;
// [Serializable]
// float attackRadius;
//
// [Header("Movement")]
// etc...
//
// Variables that require extra information given to designers should have extra tooltip information
// given for easier understanding.
// example:
// [Header("Attacking")]
// [Tooltip("Number of attacks per second")]
// [Serializable]
// float attackSpeed;
// [Tooltip("The max range of unit's attack")]
// [Serializable]
// float attackRadius;
//
//
// This becomes extra work to declare individual variables, but the result allows for easier
// readability inside the editor itself.
//======================================================================================================
