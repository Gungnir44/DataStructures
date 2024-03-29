-- The software suite consists of ProductDistributionMain,
-- Food_DataStructures, Stats_FoodDistribution, GateKeeperService,
-- Distribution_Service, CircularQue and Food_SalesService.
--
-- With the exception of ProductDistributionMain this suite represents the software
-- to manage an "embedded" planetary system food receiving and distribution system.
-- The Distribution_Service module will be discarded once the embedded software required to
-- manage the physical system is complete and installed.
--
--** This implementation of the circular queue sacrifices run-time allowing complete utilization of all queue space.
-- Message should not be sent without verifying the queue has available storage space!!!!

with Food_DataStructures; use Food_DataStructures;
with Ada.text_IO; use Ada.Text_IO;


generic
   type message is private;
   with function ">" (Left, Right : message) return Boolean;
   with function "=" (left, Right : message) return Boolean;
   capacity: Natural;

package CircularQue is

   procedure acceptMessage(msg: in message);

   procedure retrieveMessage(msg: in out message);

   function circularQueEmpty return Boolean;

   function circularQueFull return Boolean;

   --Add method (function or procedure) for inserting at front of queue here and in body.

end CircularQue;
