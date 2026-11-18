# Finger Tendon Vibration: Finger Movement Illusions for Immersive Virtual Object Interaction

Unity VR scenes developed for the research presented at **ACM CHI 2026**.

[Paper (arXiv)](https://arxiv.org/abs/2602.08201) | [Project Page](https://ftv.hcitech.org/)

## Abstract

Finger tendon vibration (FTV) can induce illusory finger flexion and extension without physical movement. This work investigates short-duration FTV as kinesthetic feedback for virtual object interaction, evaluates rendering strategies across six VR interaction scenarios, and examines its effects on the VR experience. The present repository provides Unity scenes developed during this research.

![Overview of finger tendon vibration and the six VR scenarios](https://arxiv.org/html/2602.08201v1/fig/Figure1.png)


## VR Application Scenes

![Six VR interaction scenarios using finger tendon vibration](https://arxiv.org/html/2602.08201v1/fig/Figure7.png)


## Implementation

- **Unity:** 2022.3.36f1 (Universal Render Pipeline 14.0.11)
- **XR:** XR Interaction Toolkit 2.5.4; XR Hands 1.4.1; OpenXR 1.11.0
- **Scenes:** `Assets/Scenes/` (button, slider, and virtual objects)
- **Vibration hardware used in the study:** Two Vybronics VZ7AL2B169208T eccentric rotating mass (ERM) motors, a DRV8835 motor driver, and a Seeed Studio XIAO nRF52840 microcontroller. The motors were operated at 80 Hz with a ±1 mm vibration displacement amplitude.

![FTV actuator placement and hardware configuration](https://arxiv.org/html/2602.08201v1/fig/Figure2.png)
