CREATE FUNCTION validate_control_times()
RETURNS TRIGGER AS $$
BEGIN
  IF NEW.start_time > NEW.end_time THEN
    RAISE EXCEPTION 'Start time cannot be after end time';
  END IF;
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER validate_control_trigger
BEFORE INSERT OR UPDATE ON controls
FOR EACH ROW EXECUTE PROCEDURE validate_control_times();
