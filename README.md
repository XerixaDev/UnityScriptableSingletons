## Installation
- Window > Package Manager > Install package from git URL > Copy below & paste into field.
    > https://github.com/XerixaDev/UnityScriptableSingletons.git

### How To Use
1. Create C# Script > Follow the Reference below

    `public class className : Singleton<className>`

2. Tools > Singleton Master > Refresh
3. You can now use `className.Instance` globally without it being linked to a Scene

    ## How To Use - Functions
    This package includes a couple of Monobehaviour life cycle functions that can be used in your Singleton class. The below shows the included functions.
    ### Built-in functions
    - > OnAwake()
    - > Update()
    - > OnDisable()
    - > OnDestroy()
    ### Example
    ```
    public class className : Singleton<className>
    {
        public override void OnAwake()
        {
            Debug.Log("Awake from Singleton")
        }

        public override void Update()
        {
            Debug.Log("Update from Singleton");
        }
    }
    ```

