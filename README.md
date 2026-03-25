# apbd-2026-cw3
Użyłem klas abstrakcyjnych dla użytkownika oraz sprzętu, a ich określone typy były klasami dziedziczącymi po tych typach (zastosowano polimorfizm), w klasach abstrakcyjych zawarłem potrzebne pola property/metody potrzebne do ich implementacji dla okreśłonych typów. Tzn. np. w klasie User UserType oraz ActiveRentalLimit zostają określone dla konkretnego typu użytkownika.
Projekt został podzielony na klasy, gdzie główna logika biznesowa znalazła się w klasie RentalService, konsolowy interfejs, gdzie przyjmowane jest wejścia i wyświetlane wyjście, znalazł się w klasie Program, a błędom biznesowym został przyznany niestandardowy wyjątek RentalException.

Instrukcja uruchomienia:
Po skompilowaniu i uruchomieniu aplikacji konsolowej należy podążąć zgodnie ze wskazówkami wyświetaljącymi się na ekranie.
