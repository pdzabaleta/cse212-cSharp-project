public class FeatureCollection
{
    public List<Feature> Features { get; set; }
}

// Class representing an individual earthquake in the list
public class Feature
{
    // Each "feature" has a "properties" object that contains the details
    public Properties Properties { get; set; }
}

// Class containing the specific properties of an earthquake
public class Properties
{
    // The magnitude of the earthquake
    public decimal Mag { get; set; }
    // The name of the place where it happened
    public string Place { get; set; }
}