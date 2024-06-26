﻿Feature: Движимые объекты
	

@movables
Scenario: Перемещение объекта
	Given Тело находится в точке (12, 5) пространства
	And Имеет скорость (-5, 2)
	When Выполняется операция движения тела
	Then Тело имеет новые координаты (7, 7)

Scenario: Попытка сдвинуть объект с нечитаемым положением
	Given Тело, у которого невозможно определить положение в пространстве
	And Имеет скорость (-5, 2)
	When Выполняется операция движения тела
	Then Выброшено исключение

Scenario: Попытка сдвинуть объект с нечитаемым вектором скорости
	Given Тело находится в точке (12, 5) пространства
	And Скорость Тела нечитаема
	When Выполняется операция движения тела
	Then Выброшено исключение

Scenario: Попытка сдвинуть объект с неизменяемым положением
	Given Тело находится в точке (12, 5) пространства
	And Имеет скорость (-5, 2) 
	And Невозможно установить новое положение в пространстве
	When Выполняется операция движения тела
	Then Выброшено исключение

Scenario: Разная размерность векторов скорости и положения
	Given Тело находится в точке (12, 5) пространства
	And Имеет скорость (-5, 2, 4) 
	When Выполняется операция движения тела
	Then Выброшено исключение
