d# Beerwulf backend developer assessment
Thank you for taking the time to complete this coding assessment. :) 

This assessment is purposefully simple which means that we'll be paying special mind to:
1. best practices;
2. design patterns;
3. code clarity; and 
4. project structure. 

Try to stand out by delivering the best you possibly can to show off your skills. 

## Assessment details
- Spend around `3 hours` on this assessment;
- Use the template provided to complete the assessment (`BeerwulfAssessment.Zip`);
- Feel free to use `//comments` to show us the things you *would have* done if you had more time;
- Once you're done, zip the latest solution and send us the Google Drive, Dropbox, or OneDrive link;
- VERY IMPORTANT! Your code MUST compile and run without requiring any configuration changes. That means, we are not going to update any packages, run any commands nor change any connection strings. Pressing `F5` should launch your application.

# Coding assessment: CheckoutAPI
We would like to launch a `version 2.0` of our webshop. As such, we've asked you, a talented individual when it comes to the craft of writing code, to help us out.

Your task is to create a new API to facilitate the following: 
1. get a list of all products;
2. get 1 specific product;
3. place a product in a shopping cart;
5. get the shopping cart summary and conditionally apply any discounts to the order total depending on the items in the cart.

Our product assortment includes the following:
1. Heineken Lager - $1.99
2. Uiltje IPA - $2.49
3. Bud Light - $0.99
4. Mikkeler IPA - $16.99

Beerwulf offers 3 discount rules:
- If a customer gets more than 2 Heineken Lagers, they get 1 free for each 2 in the cart;
- If a customer gets more than 2 Bud Lights, they get 20% off on all Bud Light beers in the cart;
- If a customer gets an Uiltje IPA and a Mikkeler IPA, they get $5 off the order total.

IMPORTANT! More than 1 rule can apply to the cart.

## Assessment requirements
We would like your solution to include the following:
- Data persistence. Feel free to use any solution for persistence, as long as it's **in-memory** (like **EF Core in-memory provider** or **Dapper**);
- Coding async all the way;
- Maintaining clean code on the Controller layer;
- Separating Service and Repository layers;
- Having unit tests to show your discount logic is working is a bonus;
- Maintaining simple and clear documentation on the SwaggerUI.

## Last words
Please use the project skeleton (`BeerwulfAssessment.zip`) as a starting point, but don't be afraid to show off your solution architecture skills by creating a clever project scaffolding.

You can use the following test cases to check whether your discount logic is accurate. Here's a thought - maybe include some unit tests to prove these cases. ;) 

```
cart 1: [2x "Heineken Lager", 1x "Mikkeler IPA"] -> $20.97
cart 2: [5x "Heineken Lager", 1x "Mikkeler IPA", 1x "Bud Light"] -> $23.95
cart 3: [2x "Uiltje IPA", 8x "Bud Light"] -> $11.316
cart 4: [1x "Heineken Lager", 1x "Bud Light", 1x "Uiltje IPA", 1x "Mikkeler IPA"] -> $17.46
cart 5: [3x "Heineken Lager", 2x "Uiltje IPA", 1x "Mikkeler IPA"] -> $20.95
```

Good luck, potential wulfie! :)