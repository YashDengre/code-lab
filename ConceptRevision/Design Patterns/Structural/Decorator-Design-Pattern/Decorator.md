### Decorator Pattern:


#### It helps to add additional behavior to the objects/classes.

- We can create 'Decorator Pattern' with:
  - Interface Only {Pros: 'Simple, Flexible, No inheritance chain', Cons:'Code duplication,each decorator must forward all methods manually, etc.'}
  - Normal Concreate Class {Possible but risky because people can create the object of this like new Base(null), which will break decorator chain and now decorator base is not supported to be instansiated.}
  - Abstract Class {Bast and recommended option -  Enterprise verions}

  ** Best industry practice is to use Abstract class for base decorator.
- We create a base decorator which have default behavior implemented
- We add new decorators by inheriting and implementing the base decorator

- Q: Why use abstract class for base decorator?
  - Abstract class is not Mandatory, it is a design conveienience, we have three options as mentioned above: Interfaces, Concreate class and Abstract class {Refer above points.}
- Q:Why Create Base Decorator:
	- Without base decorator, all the decorators must have to implement the base methods which are part of base object on which we are trying to apply decoration.
	- Base decorator solves, centralized forwarding of the logic, new decorators override only when needed.
	- It maintains the structure consistency: Every decorator has inner reference (base object eg. service), forwards same way.
	- It has Future Proffing:
		- eg: Add new method in the interface?
			- Only update base decorator once.
			- Without base decorator: we need to update all decorators.
- Q:Why Methods are marked virtual in Base Decorator?
	- Decorator relies on Selective Override:
	- ' LoggingDecorator → override GetAsync
ValidationDecorator → override CreateAsync'
	- If base method is not virtual: decorator cannot override behavior, and decorator becomes useless.
	- Default Behavior => Forward Call
	- Custome Behavior => Override
	- Virutal allows: Partial behavior extension, layring, interception.
	- Same mechanism used in: ASP.NET Fitlers, MediatR pipeline, EF Interceptors, HttpClient Handlers.


	### If we/consumer can pass the inner reference in concrete implementation then will also behave like the abstract one?
	- The short truth (no hand-waving)
		- The problem is not that the customer “will” pass the inner reference.
		- The problem is that a concrete base class makes it possible and valid-looking to do so.
	That’s it. That’s the whole reason.
		- ### Why would the consumer pass the inner reference?
			- They usually wouldn’t on purpose.
			- But APIs are not designed for good intentions — they’re designed for:
				- misunderstanding
				- new team members
				- future maintainers
				- copy-paste usage
				- DI containers auto-wiring things
			- services.AddTransient<IComponent, DecoratorBase>()
			- This is perfectly legal if DecoratorBase is concrete.
				- Now suddenly:
					- no extra behavior
					- pointless wrapper
					- design invariant broken
					- nobody notices until later
				- With abstract → this mistake is impossible.
		- ### If they pass inner ref, concrete behaves like abstract anyway
			- Technically? Yes.
			- Semantically? No.
			- we’re mixing runtime behavior with type meaning.
			- Compare: new DecoratorBase(realComponent) VS new LoggingDecorator(realComponent)
			- They do the same thing today, but:
				- one claims to be complete
				- one claims to be a specialization
			- Types are promises.
			- A concrete class promises: “This object is meaningful as-is.”
			- An abstract class promises: “This object is incomplete by itself.”
			- That difference matters even if the code runs the same.
			
		- ### The real invariant Decorator enforces
			- The decorator pattern has a hidden rule:
			- ### `Every decorator must add or modify behavior`
			- A concrete base class cannot enforce this rule.
			- Abstract base does, indirectly:
				- you must subclass
				- you must make a decision about behavior
				- you cannot “accidentally” use the shell

		- ### Think in terms of “illegal states”
			- Good OO design tries to make illegal states unrepresentable.
			- With concrete base: DecoratorBase is used as a real decorator ❌
			- With abstract base: DecoratorBase used directly → impossible ✅
			- Even if you personally would never do it — the type system now guards it.
		- ### Why this matters in real systems (not textbooks)
			- In real codebases:
				- teams change
				- patterns are half-remembered
				- DI does magic behind the scenes
				- people search “Decorator” and autocomplete the base class
			- Abstract base:
				- blocks misuse at compile time
				- documents intent without comments
				- removes a whole category of bugs
			- Concrete base:
				- relies on discipline
				- discipline always leaks