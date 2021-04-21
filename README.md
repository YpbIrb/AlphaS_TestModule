# AlphaS_TestModule
Модуль для тестирования работоспособности клиентского приложения системы AlphaS.
Если вы хотите, чтобы ваша программа могла выступать как модуль в данной системе, можете просто вставить 2 функции из скрипта /Assets/Scripts/TestModuleController.cs в работу своей программы.
SendResultsWithNamedPipes(Dictionary<string, string> results) - для отправки результатов работы модуля.
GetNamedArgumentsFromConsole() - для получения начальных аргументов для модуля из консоли.

Также в ReadMe файле к вашему модулю вы должны указать название модуля, входные и выходные переменные(название, дефолтное значение, и описание), название .exe файла, и описание модуля.

Пример описания модуля :

ModuleName : TestModule
InputVariables : first 0 первый множитель
                 second 0 второй множитель
                  
OutputVariables : mult 0 возвращает произведение двух входных переменных
                  div 0 возвращает результат от деления first / second
PathToExe : TestModule
Description : Module for testing AlphaS Client app.
