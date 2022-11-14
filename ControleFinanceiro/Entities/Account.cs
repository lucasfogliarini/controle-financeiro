﻿namespace ControleFinanceiro.Entities
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
