﻿# Warning Suppressor
## EN-US
#### ⚠️ Attention
> The development of this package is done during my free time.</br>
> If you find a bug and that ritual opens an issues, depending</br>
> free time that I have, I'll act like justice is slow but it doesn't fail.</br>
> And if you have a feedback and just open an issues too.
### Installation
Installation using scoped registries</br>
![](Documentation~/Image/install_npm_NoWarng.png)</br>
Installation using add package from git url</br>
![](Documentation~/Image/install_github_NoWarng.png)
### Global no warning
Global warning suppressor is used to generate warning suppression for all assemblies.<br/>
![](Documentation~/Image/UseGlobalNoWar.png)<br/>
Enter CheckID in the field below 'No War' and end with ';'.<br/>
![](Documentation~/Image/UseGlobalNoWar2.png)<br/>
```
exp:
	IDE0017;IDE0018;IDE0019;
```
### Individual no warning
Local warning suppressor creates specific deletions for each assembly.<br/>
![](Documentation~/Image/IndividualNoWar.png)<br/>
#### Applay no warning
This option ensures that local suppression is applied at the expense of global suppression.
#### Applay global no warning
This option applies global suppression if local suppression is not checked.
#### No War
Enter CheckID in the field below 'No War' and end with ';'.
```
exp:
	IDE0017;IDE0018;IDE0019;
exp[For object members]:
	//~F:For field
	//~P:For property(Including indexers)
	//~T:For type(As class, delegate, enum, interface, and struct)
	//~M:To method(Including constructors, deconstructors, and operators)
	//~E:For event
	IDE0051>~M:NameSpaceTest.ClasseTest.MethodTest;IDE0051>~M:NameSpaceTest.ClasseTest.MethodTest(System.Int32);
```
#[If you want to help me](https://www.paypal.com/donate/?business=VN4RAWDSA2PBA&no_recurring=0&currency_code=BRL)
## PT-BR
#### ⚠️ Atenção
> O desenvolvimento deste pacote é feito durante meu tempo liver.</br>
> Se econtrar algum bug e aquele ritual abrir uma issues, dependendo</br>
> tempo liver que eu tenha agirei que nem a justiça tarda mas não falha.</br>
> E se tiver um feedback e só abrir um issues também.
### Instalação
Instalação usando scoped registries</br>
![](Documentation~/Image/install_npm_NoWarng.png)</br>
Instalação usando add package from git url</br>
![](Documentation~/Image/install_github_NoWarng.png)
### Global no warning
O supressor de aviso global é usado para gerar supressão de aviso para todos os assemblies.<br/>
![](Documentation~/Image/UseGlobalNoWar.png)<br/>
Digite CheckID no campo abaixo de 'No War' e termine com ';'.<br/>
![](Documentation~/Image/UseGlobalNoWar2.png)<br/>
```
exp:
	IDE0017;IDE0018;IDE0019;
```
### Individual no warning
Supressor de aviso local cria supressões especificas para cada montagem.<br/>
![](Documentation~/Image/IndividualNoWar.png)<br/>
#### Applay no warning
Essa opção garante que a supressão local seja aplicada em detrimento da supressão global.
#### Applay global no warning
Essa opção aplica a supressão global caso a supressão local não esteja marcada.
#### No War
Digite CheckID no campo abaixo de 'No War' e termine com ';'.
```
exp:
	IDE0017;IDE0018;IDE0019;
exp[Para membros do objeto]:
	//~F:Para campo
	//~P:Para propriedade(Incluindo indexadores)
	//~T:Para tipo(Como class, delegate, enum, interface e struct)
	//~M:Para método(Incluindo construtores, desconstrutores e operadores)
	//~E:Para evento
	IDE0051>~M:NameSpaceTest.ClasseTest.MethodTest;IDE0051>~M:NameSpaceTest.ClasseTest.MethodTest(System.Int32);
```

#[Caso queira me ajudar](https://www.paypal.com/donate/?business=VN4RAWDSA2PBA&no_recurring=0&currency_code=BRL)