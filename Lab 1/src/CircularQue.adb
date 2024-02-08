package body CircularQue is

   package IntIO is new Ada.Text_IO.Integer_IO(Integer);
   use IntIO;

	type Vector is array (Integer range <>) of message;
	subtype Index is Integer range 1..capacity-1;
	last : Index := 1;
	first : Index := 1;
	box : Vector (0 .. capacity-1); -- circular buffer

      procedure acceptMessage(msg: in message) is  -- ** modify for placing in dual stacks
      Temp: Index := 1;
      begin
         if Temp > last then put("ERROR - Message rejected - queue is full!"); new_line(2);
         else
            Temp := first;
         end if;
         --
         while Temp <= last and then msg > box( Temp ) loop
            Temp := Temp + 1;
         end loop;
         --
         for I in reverse Temp .. last loop
            box( I + 1 ) := box( I );
         end loop;
         --
         last := last + 1;
         box( Temp ) := msg;
      end acceptMessage;

   procedure retrieveMessage(msg: in out message) is  -- ** binary search
      LB: Natural := 1; UB: Natural := last;
      Mid: Integer := (UB + LB)/2;
   begin
      while UB > LB loop
         exit when LB > UB;
         if box( first ) > msg then
            UB := Mid - 1;
         elsif msg > box( first ) then
            LB := Mid + 1;
         else
            if msg = box( Mid ) then
               msg := box( Mid );
               for J in reverse Mid .. last loop
                  box( J ) := box( J + 1 );
               end loop;
               exit;
            else
               put("Sorry, no food packets of the <desired type> are currently available"); new_line(2);
               msg := box( last );
               last := last - 1;
               exit;
            end if;
         end if;
      end loop;

   end retrieveMessage;

   function CircularQueEmpty return Boolean is
   begin
      if last > 0 then
         return False;
      else
         return True;
      end if;
   end CircularQueEmpty;

   function CircularQueFull return Boolean is  -- ** modify for dual stacks
      begin
      if last < capacity - 1 then
         return False;
      else
         return True;
      end if;
   end CircularQueFull;

end CircularQue;


