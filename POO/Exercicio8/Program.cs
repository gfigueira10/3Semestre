﻿using Exercício8;

IAutenticavel usuario = new Usuario();
IAutenticavel admin = new Administrador();

Console.WriteLine($"usuario.Autenticar{1234}");  // true
Console.WriteLine($"admin.Autenticar{admin}");   // true
Console.WriteLine($"admin.Autenticar{1234}");   // false