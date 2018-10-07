# Miho PC Remote - Windows Interface #

_Miho_ is an open source PC remote control. It implements input controls.
The goal is to make it easier to use certain PC functions when keyboard
and mouse is too inconvenient (such as when connected to a TV).

The interface provides a means to more easily handle the driver. It sets
up and runs the driver in the background, while providing a convenient
means of configuring it.

The driver - the part that actually generates the input - is located in
a different project - [`miho-driver`](https://github.com/NelsonCrosby/miho-driver).
The actual "remote" part is implemented by a client app, also located in
a separate project:

- Android: [`miho-client-android`](https://github.com/NelsonCrosby/miho-client-android)

The name is a reference to Miho Noyama, a character from _A Channel_. I won't
explain the joke, if you wanna understand, go watch it. Or not, I'm not your
mother.


> ## WARNING ##
>
> Miho is currently not only prototype-quality, but also very much insecure.
> Specifically, the protocol is raw and unencrypted, which carries risks.
> I do plan to encrypt it soon, but right now that has not yet happened.


## TODO ##

- [ ] Create a Windows Service for the driver
- [ ] Run and configure the service via a graphical interface
- [ ] Generate and display a QR code with connection details

### Future directions ###

- A sleeker UI than I will probably be able to get with WinForms
  (maybe WPF or something; I don't know Windows stuff)
