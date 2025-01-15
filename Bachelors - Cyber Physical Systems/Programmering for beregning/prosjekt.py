import math
import numpy as np
import matplotlib.pyplot as plt

# oppgave 1
# def age():
#     alder = int(input("Hvilket år er du født? "))
#     print("Du fyller", (2022 - alder), "i år!")


# age()


# oppgave 2
# def pizza_estimator():
#     antall_elever = int(input("Skriv inn antall elever: "))
#     print("Antall pizza: ", math.ceil(antall_elever * 0.5))

# pizza_estimator()

# oppgave 3

# def deg_to_rad():
#     v_g = float(input("Skriv inn gradtallet:"))
#     v_r = v_g * np.pi / 180
#     print(v_g, "grader =", round(v_r, 2), "radianer")


# deg_to_rad()


# oppgave 4
# def trekant_og_halvsirkel(diameter, høyde):
#     areal_halvsirkel = np.pi * ((diameter * 0.5) ** 2)
#     areal_trekant = diameter * høyde * 0.5

#     omkrets_halvsirkel = np.pi * (diameter * 0.5)
#     hypotenusen = np.sqrt(diameter**2 + høyde**2)

#     print(
#         "Sammenslått areal = ",
#         round(areal_halvsirkel + areal_trekant, 3),
#         "\n" "Ytre omkrets = ",
#         round(omkrets_halvsirkel + høyde + hypotenusen, 3),
#     )


# trekant_og_halvsirkel(2, 5)


# oppgave 7


def plot_func():
    x = np.linspace(-10, 10, 200)
    y = eval(str(-(x**2) - 5))

    plt.plot(y)
    plt.show


plot_func()
