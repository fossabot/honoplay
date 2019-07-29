﻿namespace Honoplay.Domain.Entities
{
    public class Training : BaseEntity
    {
        public int Id { get; set; }
        public int TrainingSeriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public TrainingSeries TrainingSeries { get; set; }
    }
}
