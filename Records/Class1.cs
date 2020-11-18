using System;

namespace Records
{

    public record Adult {
      public int ID { get; init; }
      public string FirstName { get; init; }
      public string LastName { get; init; }
      public string Address { get; init; }}

    public record Child {public int ID { get; set; }}

    public record Dog {
      public int ID { get; init; }
      public string Name { get; init; }}

}
