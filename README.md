# AlphaS_TestModule
Модуль для тестирования работоспособности клиентского приложения системы AlphaS.  

Для того, чтобы модуль мог корректно работать с системой, нужно удовлетворение следующих условий:
1. Создание описания модуля в веб-службе системы.
2. Помещение ярлыка исполняемого файла в папку C:\Modules
3. Обеспечение поддержки следующего протокола передачи данных:
    * Входные переменные передаются в модуль как именованные переменные при запуске .exe файлов. Пример консольной команды для windows cmd, имитирующей запуск системой модуля с названием TestModule, и входными переменными first и second со значениями 2 и 5: “TestModule.exe.lnk first=2 second=5”
Кроме входных переменных, обозначенных в модуле, в каждый модуль будут передаваться id испытуемых, обозначенные переменными “first_part” и “second_part”. Если информации об id испытуемых в системе нет, то тогда значения данных переменных будут -1 и -2 соответственно.
    * Выходные данные модуль должен отдать, используя систему именованных каналов(named pipes). Название используемого системой канала: “AlphaS”. Первым сообщением должно идти название модуля. Последующими сообщениями должны идти пары переменная=значение. Последним сообщением от модуля должно идти сообщение “End”

Для обеспечения третьего пункта, вы можете просто вставить 2 функции из скрипта [/Assets/Scripts/TestModuleController.cs](https://github.com/YpbIrb/AlphaS_TestModule/blob/main/Assets/Scripts/TestModuleController.cs) в работу своей программы.  
SendResultsWithNamedPipes(Dictionary<string, string> results) - для отправки результатов работы модуля.  
GetNamedArgumentsFromConsole() - для получения начальных аргументов для модуля из консоли.  
