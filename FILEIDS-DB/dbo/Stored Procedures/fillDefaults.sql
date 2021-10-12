﻿--create procedure fillDefaults
--as
--begin

--	/* Valores insertados relevantes*/
--	insert into [metadata]
--	values('Carcasa Herramienta Gripper', 'Gripper Tool Case', '', '3', '');
--	insert into [metadata]
--	values('Frontal Carcasa Herramienta Gripper', 'Gripper Tool Case Front', '', '2', '');
--	insert into [metadata]
--	values('Carcasa Inferior Herramienta Gripper', 'Gripper Tool Lower Casing', '', '2', '');
--	insert into [metadata]
--	values('Carcasa Posterior Herramienta Gripper', 'Gripper Tool Back Cover', '', '2', '');
--	insert into [metadata]
--	values('Caja de paso carcasa herramienta gripper', 'Griper tool cable casing', '', '2', '');
--	insert into [metadata]
--	values('Herramienta Gripper - PDI', 'General Arrangement - Gripper Tool', '', '2', '');
--	insert into [metadata]
--	values('Dedos de gripper', 'Grip fingers', '', '2', '');
--	insert into [metadata]
--	values('Buje lineal de grafito', 'Dry graphite Linear bearing', '', '2', '');
--	insert into [metadata]
--	values('Soporte copla de oxigeno', 'Oxy coupling support', '', '2', '');
--	insert into [metadata]
--	values('Acople de oxigeno', 'Oxy Coupling', '', '2', '');
--	insert into [metadata]
--	values('Chasis Herramienta Gripper', 'Gripper Tool Chassis', '', '2', '');
--	insert into [metadata]
--	values('Porta bujes', 'Bearing housing', '', '2', '');
--	insert into [metadata]
--	values('Chasis Superior herramienta gripper', 'Gripper tool upper chassis', '', '2', '');
--	insert into [metadata]
--	values('Soporte Eje Lateral', 'Side Shaft Support', '', '2', '');
--	insert into [metadata]
--	values('Soporte de Sensor Ultrasonico', 'Ultrasound sensor bracket', '', '2', '');
--	insert into [metadata]
--	values('Tapa superior carcasa gripper', 'Gripper tool case upper plate', '', '2', '');
--	insert into [metadata]
--	values('Eje lateral', 'Side shaft', '', '2', '');
--	insert into [metadata]
--	values('Placa de sensor', 'Sensor support', '', '2', '');
--	insert into [metadata]
--	values('Escudo interno frontal', 'Inner front shield', '', '2', '');
--	insert into [metadata]
--	values('Soporte Eje Central', 'Center shaft support', '', '2', '');
--	insert into [metadata]
--	values('Eje Central', 'Center Shaft', '', '2', '');
--	insert into [metadata]
--	values('ANILLO CENTRADOR', 'CENTERING RING', '', '2', '');
--	insert into [metadata]
--	values('Placa de sensado', 'Proximitor Plate', '', '2', '');
--	insert into [metadata]
--	values('Pieza Base lado Bujes', 'Base - Bushing side', '', '1', '');
--	insert into [metadata]
--	values('Pasador', 'Pin', '', '1', '');
--	insert into [metadata]
--	values('Pieza Base lado Bujes', 'Base - Bushing side', '', '2', '');
--	insert into [metadata]
--	values('Pasador', 'Pin', '', '2', '');
--	insert into [metadata]
--	values('Bujes', 'Bushing', '', '1', '');
--	insert into [metadata]
--	values('Bujes', 'Bushing', '', '2', '');
--	insert into [metadata]
--	values('Herramienta Toma Core Vertical - Derecho', 'Vertical core grip tool - Right side', '', '3', '');
--	insert into [metadata]
--	values('Herramienta Toma Core Vertical - Izquierdo', 'Vertical core grip tool - Left Side', '', '3', '');
--	insert into [metadata]
--	values('Herramienta Toma Core Vertical - Derecho', 'Vertical core grip tool - Right side', '', '2', '');
--	insert into [metadata]
--	values('Herramienta Toma Core Vertical - Izquierdo', 'Vertical core grip tool - Left Side', '', '2', '');
--	insert into [metadata]
--	values('Pieza Base lado Pasador', 'Base - Pin side', '', '1', '');
--	insert into [metadata]
--	values('Pieza Base lado Pasador', 'Base - Pin side', '', '2', '');
--	insert into [metadata]
--	values('Cuchilla', 'Blade', '', '1', '');
--	insert into [metadata]
--	values('Cuchilla', 'Blade', '', '2', '');
--	insert into [metadata]
--	values('Goma', 'Rubber Grip', '', '1', '');
--	insert into [metadata]
--	values('Goma', 'Rubber Grip', '', '2', '');
--	insert into [metadata]
--	values('Pasador de acero', 'Steel Pin', '', '1', '');
--	insert into [metadata]
--	values('Chasis Herramienta 80', 'Tool 80 Chassis', '', '1', '');
--	insert into [metadata]
--	values('Bracket Herramienta 80', 'Tool 80 Bracket', '', '1', '');
--	insert into [metadata]
--	values('Shaft Herramienta 80', 'Tool 80 Shaft', '', '1', '');
--	insert into [metadata]
--	values('Chasis Herramienta 80', 'Tool 80 Chassis', '', '2', '');
--	insert into [metadata]
--	values('Bracket Herramienta 80', 'Tool 80 Bracket', '', '2', '');
--	insert into [metadata]
--	values('Shaft Herramienta 80', 'Tool 80 Shaft', '', '2', '');
--	insert into [metadata]
--	values('Ensamblaje Herramienta 80', 'Tool 80 Assembly', '', '2', '');
--	insert into [metadata]
--	values('Dedo Herramienta 80', 'Tool 80 gripper finger', '', '2', '');
--	insert into [metadata]
--	values('Dedo Herramienta 80', 'Tool 80 gripper finger', '', '1', '');
--	insert into [metadata]
--	values('Ensamblaje Taponadora 50', 'Mudgun 50 assembly', '', '3', '');
--	insert into [metadata]
--	values('Ensamblaje Taponadora 50', 'Mudgun 50 assembly', '', '2', '');
--	insert into [metadata]
--	values('Buje ', 'Bushing', '', '1', '');
--	insert into [metadata]
--	values('Extensor ', 'Extensor', '', '1', '');
--	insert into [metadata]
--	values('Buje ', 'Bushing', '', '2', '');
--	insert into [metadata]
--	values('Extensor ', 'Extensor', '', '2', '');
--	insert into [metadata]
--	values('Soporte Electroiman', 'Electromagnet Support ', '', '1', '');
--	insert into [metadata]
--	values('Gripper', 'Gripper', '', '3', '');
--	insert into [metadata]
--	values('Flange Centrador', 'Centerer Flange', '', '1', '');
--	insert into [metadata]
--	values('Proximitor', 'Position Sensor ', '', '1', '');
--	insert into [metadata]
--	values('Soporte Sensor ', 'Support Sensor', '', '1', '');
--	insert into [metadata]
--	values('SHUNK Sensor Inductivo', 'SHUNK Inductive Sensor', '', '1', '');
--	insert into [metadata]
--	values('SICK Sensor Laser ', 'SICK Laser Sensor', '', '1', '');
--	insert into [metadata]
--	values('SKF MB7 Lock washer ', 'SKF MB7 Lock washer ', '', '1', '');
--	insert into [metadata]
--	values('Tuerca SKF KM7', 'SKF KM7 Locknut', '', '1', '');
--	insert into [metadata]
--	values('Ensamblaje Herramienta 80', 'Tool 80 Assembly', '', '1', '');
--	insert into [metadata]
--	values('Soporte Electroiman', 'Electromagnet Support', '', '2', '');
--	insert into [metadata]
--	values('Flange Centrador Herramienta 80', 'Tool 80 centering flange', '', '2', '');
--	insert into [metadata]
--	values('Soporte Sensor Herramienta 80', 'Tool 80 Sensor Bracket', '', '2', '');
--	insert into [metadata]
--	values('Cabezal de Sacrificio', '', '', '1', '');
--	insert into [metadata]
--	values('Cañon ', '', '', '1', '');
--	insert into [metadata]
--	values('Proteccion  Cilindro', 'Hydraulic Cylinder Shield', '', '1', '');
--	insert into [metadata]
--	values('Eje de Fuerza', '', '', '1', '');
--	insert into [metadata]
--	values('Eje de Toma ', 'Gripper grab bar', '', '1', '');
--	insert into [metadata]
--	values('Flange', 'Flange', '', '3', '');
--	insert into [metadata]
--	values('Contrapeso', 'Counterweight', '', '1', '');
--	insert into [metadata]
--	values('Soporte Sensor ', 'Sensor bracket', '', '3', '');
--	insert into [metadata]
--	values('Extensor Cilindro', 'Hydraulic cylinder shaft extension', '', '1', '');
--	insert into [metadata]
--	values('Cabezal de Empuje ', 'Clay pushing piston', '', '1', '');
--	insert into [metadata]
--	values('Cabezal de Sacrificio', '', '', '2', '');
--	insert into [metadata]
--	values('Cañon ', '', '', '2', '');
--	insert into [metadata]
--	values('Proteccion  Cilindro', 'Hydraulic Cylinder Shield', '', '2', '');
--	insert into [metadata]
--	values('Eje de Fuerza', '', '', '2', '');
--	insert into [metadata]
--	values('Eje de Toma ', 'Gripper grab bar', '', '2', '');
--	insert into [metadata]
--	values('Flange', 'Flange', '', '2', '');
--	insert into [metadata]
--	values('Contrapeso', 'Counterweight', '', '2', '');
--	insert into [metadata]
--	values('Soporte Sensor ', 'Sensor bracket', '', '2', '');
--	insert into [metadata]
--	values('Extensor Cilindro', 'Hydraulic cylinder shaft extension', '', '2', '');
--	insert into [metadata]
--	values('Cabezal de Empuje ', 'Clay pushing piston', '', '2', '');
--	insert into [metadata]
--	values('Carcasa Herramienta Gripper', 'Gripper Tool Case', '', '1', '');
--	insert into [metadata]
--	values('Frontal Carcasa Herramienta Gripper', 'Gripper Tool Case Front', '', '1', '');
--	insert into [metadata]
--	values('Carcasa Inferior Herramienta Gripper', 'Gripper Tool Lower Casing', '', '1', '');
--	insert into [metadata]
--	values('Carcasa Posterior Herramienta Gripper', 'Gripper Tool Back Cover', '', '1', '');
--	insert into [metadata]
--	values('Caja de paso carcasa herramienta gripper', 'Griper tool cable casing', '', '1', '');
--	insert into [metadata]
--	values('Carcasa Herramienta Gripper', 'Gripper Tool Case', '', '2', '');
--	insert into [metadata]
--	values('Tapa superior carcasa gripper', 'Gripper tool case upper plate', '', '1', '');
--	insert into [metadata]
--	values('Sensor', 'Sensor', '', '1', '');
--	insert into [metadata]
--	values('Cilindro Hidraulico', 'Hydraulic Cylinder', '', '1', '');
--	insert into [metadata]
--	values('Lanza Ultraoxibar 0.5in x 3 m', 'Ultraoxibar Lance 0.5in x 3 m', '', '2', '');
--	insert into [metadata]
--	values('Lanza Ultraoxibar 0.5in x 3 m', 'Ultraoxibar Lance 0.5in x 3 m', '', '1', '');
--	insert into [metadata]
--	values('Goma sello de lanza 0.5in', '0.5in Lance rubber seal', '', '1', '');
--	insert into [metadata]
--	values('Goma sello de lanza 0.5in', '0.5in Lance rubber seal', '', '2', '');
--	insert into [metadata]
--	values('Conjunto Lanza Ultraoxibar 0.5in x 3 m', '0.5in x 3m Ultraoxibar Lance assembly', '', '3', '');
--	insert into [metadata]
--	values('Plano Conjunto Lanza Ultraoxibar 0.5in x 3 m', '0.5in x 3m Ultraoxibar Lance assembly drawing', '', '2', '');
--	insert into [metadata]
--	values('Bushing ', 'Bushing ', '', '2', '');
--	insert into [metadata]
--	values('KUKA KR150 R3700 ULTRA K', 'KUKA KR150 R3700 ULTRA K', '', '3', '');
--	insert into [metadata]
--	values('CONJUNTO PORTALANZAS 80', '80 SERIES LANCE HOLDER', '', '1', '');
--	insert into [metadata]
--	values('CONJUNTO PORTALANZAS 80', '80 SERIES LANCE HOLDER', '', '2', '');
--	insert into [metadata]
--	values('Estructura 01 Portalanzas', 'Structure 01 - Lance Holder', '', '1', '');
--	insert into [metadata]
--	values('Estructura 02 Portalanzas', 'Structure 02 - Lance Holder', '', '1', '');
--	insert into [metadata]
--	values('Estructura 03 Portalanzas', 'Structure 03 - Lance Holder', '', '1', '');
--	insert into [metadata]
--	values('Estructura 01 Portalanzas', 'Structure 01 - Lance Holder', '', '2', '');
--	insert into [metadata]
--	values('Estructura 02 Portalanzas', 'Structure 02 - Lance Holder', '', '2', '');
--	insert into [metadata]
--	values('Estructura 03 Portalanzas', 'Structure 03 - Lance Holder', '', '2', '');
--	insert into [metadata]
--	values('Layout General Boliden Ronnskar', 'General Arrangement Boliden Ronnskar', '', '3', '');
--	insert into [metadata]
--	values('GRID Boliden Ronnskar', 'GRID Boliden Ronnskar', '', '1', '');
--	insert into [metadata]
--	values('Ensamblaje Portalanzas', 'Oxy lance holder assembly', '', '1', '');
--	insert into [metadata]
--	values('Portaherramientas 90', '90 Series toolholder', '', '1', '');
--	insert into [metadata]
--	values('Portaherramientas 90', '90 Series toolholder', '', '2', '');
--	insert into [metadata]
--	values('Barreras duras Boliden Ronnskar', 'Hard Barriers Boliden Ronnskar', '', '1', '');
--	insert into [metadata]
--	values('Barreras duras Boliden Ronnskar', 'Hard Barriers Boliden Ronnskar', '', '2', '');
--	insert into [metadata]
--	values('Caja de paso carcasa gripper', 'Gripper junction box', '', '1', '');
--	insert into [metadata]
--	values('Chasis Herramienta Gripper', 'Gripper Tool Chassis', '', '1', '');
--	insert into [metadata]
--	values('Dedos de gripper', 'Gripper fingers', '', '1', '');
--	insert into [metadata]
--	values('Soporte copla de oxigeno', 'Oxy coupling support', '', '1', '');
--	insert into [metadata]
--	values('ACOPLE DE OXIGENO', 'Oxy Coupling', '', '1', '');
--	insert into [metadata]
--	values('PORTA BUJES', 'Bearing housing', '', '1', '');
--	insert into [metadata]
--	values('Chasis Superior herramienta gripper', 'Gripper tool upper chassis', '', '1', '');
--	insert into [metadata]
--	values('Soporte Eje Lateral', 'Side Shaft Support', '', '1', '');
--	insert into [metadata]
--	values('Soporte de Sensor Ultrasonico', 'Ultrasound sensor bracket', '', '1', '');
--	insert into [metadata]
--	values('Eje lateral', 'Side shaft', '', '1', '');
--	insert into [metadata]
--	values('Placa de sensor', 'Sensor support', '', '1', '');
--	insert into [metadata]
--	values('Soporte Eje Central', 'Center shaft support', '', '1', '');
--	insert into [metadata]
--	values('Eje Central', 'Center Shaft', '', '1', '');
--	insert into [metadata]
--	values('TAPA SENSOR', '', '', '1', '');
--	insert into [metadata]
--	values('TAPA SENSOR', '', '', '2', '');
--	insert into [metadata]
--	values('Placa de sensado', 'Proximitor Plate', '', '1', '');
--	insert into [metadata]
--	values('ANILLO CENTRADOR', 'CENTERING RING', '', '1', '');
--	insert into [metadata]
--	values('BUJE SIN LUBRICACION', 'BUSHING OIL FREE', '', '1', '');
--	insert into [metadata]
--	values('CORTALLAMAS', 'FIREFIGHTERS', '', '1', '');
--	insert into [metadata]
--	values('BUJE CON BRIDA SIN LUBRICACION', 'BUSHING OIL FREE FLANG', '', '1', '');
--	insert into [metadata]
--	values('SENSOR ULTRASONICO', 'ULTRASONIC SENSOR', '', '1', '');
--	insert into [metadata]
--	values('SENSOR INDUCTIVO', 'INDUCTIVE SENSOR', '', '1', '');
--	insert into [metadata]
--	values('SENSOR INDUCTIVO DE LANZA', 'inductive lance sensor', '', '1', '');
--	insert into [metadata]
--	values('SEGURO EJE LATERAL', 'AXIS LATERAL SAFE', '', '1', '');
--	insert into [metadata]
--	values('Resorte carga ligera', 'light load spring', '', '1', '');
--	insert into [metadata]
--	values('Ensamblaje Picadora 60', '60 Series demolition tool', '', '3', '');
--	insert into [metadata]
--	values('Ensamblaje Picadora 60', '60 Series demolition tool', '', '2', '');
--	insert into [metadata]
--	values('Eje de toma picadora 60', '60 Series tool grab bar', '', '1', '');
--	insert into [metadata]
--	values('Eje de toma picadora 60', '60 Series tool grab bar', '', '2', '');
--	insert into [metadata]
--	values('Picadora HM1307C Makita', 'Makita HM1307C Demolition Hammer', 'HM1307C', '1', '');
--	insert into [metadata]
--	values('Chasis picadora 60', '60 Series tool chassis', '', '1', '');
--	insert into [metadata]
--	values('Chasis picadora 60', '60 Series tool chassis', '', '2', '');
--	insert into [metadata]
--	values('Resorte Delantero Picadora 60', '60 series tool front spring ', '', '1', '');
--	insert into [metadata]
--	values('Resorte trasero Picadora 60', '60 series tool rear spring ', '', '1', '');
--	insert into [metadata]
--	values('Resorte Delantero Picadora 60', '60 series tool front spring ', '', '2', '');
--	insert into [metadata]
--	values('Resorte trasero Picadora 60', '60 series tool rear spring ', '', '2', '');
--	insert into [metadata]
--	values('Soporte trasero Picadora 60', '60 series tool backplate', '', '1', '');
--	insert into [metadata]
--	values('Soporte trasero Picadora 60', '60 series tool backplate', '', '2', '');
--	insert into [metadata]
--	values('Soporte delantero Picadora 60', '60 series tool frontplate', '', '1', '');
--	insert into [metadata]
--	values('Soporte delantero Picadora 60', '60 series tool frontplate', '', '2', '');
--	insert into [metadata]
--	values('Plancha superior carcasa picadora 60', '60 series tool cover top plate', '', '1', '');
--	insert into [metadata]
--	values('Plancha superior carcasa picadora 60', '60 series tool cover top plate', '', '2', '');
--	insert into [metadata]
--	values('Cubierta inferior picadora 60', '60 series tool lower cover', '', '1', '');
--	insert into [metadata]
--	values('Cubierta inferior picadora 60', '60 series tool lower cover', '', '2', '');
--	insert into [metadata]
--	values('Cubierta frontal picadora 60', '60 series tool front cover', '', '1', '');
--	insert into [metadata]
--	values('Cubierta frontal picadora 60', '60 series tool front cover', '', '2', '');
--	insert into [metadata]
--	values('Tapa frontal picadora 60', '60 series tool front lid', '', '1', '');
--	insert into [metadata]
--	values('Tapa frontal picadora 60', '60 series tool front lid', '', '2', '');
--	insert into [metadata]
--	values('Tapa posterior picadora 60', '60 series tool rear lid', '', '1', '');
--	insert into [metadata]
--	values('Tapa posterior picadora 60', '60 series tool rear lid', '', '2', '');
--	insert into [metadata]
--	values('Tapa superior posterior picadora 60', '60 series tool upper rear cover', '', '1', '');
--	insert into [metadata]
--	values('Tapa superior posterior picadora 60', '60 series tool upper rear cover', '', '2', '');
--	insert into [metadata]
--	values('Tapa superior delantera izquierda picadora 60', '60 series tool upper front left cover', '', '1', '');
--	insert into [metadata]
--	values('Tapa superior delantera izquierda picadora 60', '60 series tool upper front left cover', '', '2', '');
--	insert into [metadata]
--	values('Tapa superior delantera derecha picadora 60', '60 series tool upper front right cover', '', '1', '');
--	insert into [metadata]
--	values('Tapa superior delantera derecha picadora 60', '60 series tool upper front right cover', '', '2', '');
--	insert into [metadata]
--	values('Barra interna picadora 60', '60 series tool slider shaft', '', '1', '');
--	insert into [metadata]
--	values('Barra interna picadora 60', '60 series tool slider shaft', '', '2', '');
--	insert into [metadata]
--	values('Slider picadora 60', '60 series tool slider', '', '1', '');
--	insert into [metadata]
--	values('Slider picadora 60', '60 series tool slider', '', '2', '');
--	insert into [metadata]
--	values('Buje slider picadora 60', '60 series tool slider bushing', '', '1', '');
--	insert into [metadata]
--	values('Buje slider picadora 60', '60 series tool slider bushing', '', '2', '');
--	insert into [metadata]
--	values('Contratuerca M56', 'M56 Jam nut', '', '1', '');
--	insert into [metadata]
--	values('Contratuerca M56', 'M56 Jam nut', '', '2', '');
--	insert into [metadata]
--	values('Contratuerca M36', 'M36 Jam nut', '', '1', '');
--	insert into [metadata]
--	values('Contratuerca M36', 'M36 Jam nut', '', '2', '');
--	insert into [metadata]
--	values('Resorte carga ligera corto', 'Short Light Load Spring', '', '1', '');
--	insert into [metadata]
--	values('BUJE SIN LUBRICACION', 'BUSHING OIL FREE', '', '1', '');
--	insert into [metadata]
--	values('BUJE CON BRIDA SIN LUBRICACION', 'BUSHING OIL FREE FLANG', '', '1', '');
--	insert into [metadata]
--	values('SEGURO EJE LATERAL', 'AXIS LATERAL SAFE', '', '1', '');
--	insert into [metadata]
--	values('HERRAMIENTA GRIPPER', 'GRIPPER TOOL', '', '3', '');
--	insert into [metadata]
--	values('PROTECTOR FRONTAL DERECHO', 'RIGHT FRONT PROTECTOR', '', '1', '');
--	insert into [metadata]
--	values('PROTECTOR FRONTAL DERECHO', 'RIGHT FRONT PROTECTOR', '', '2', '');
--	insert into [metadata]
--	values('PROTECTOR FRONTAL IZQUIERDO', 'LEFT FRONT PROTECTOR', '', '1', '');
--	insert into [metadata]
--	values('PROTECTOR FRONTAL IZQUIERDO', 'LEFT FRONT PROTECTOR', '', '2', '');
--	insert into [metadata]
--	values('Caja de paso carcasa herramienta gripper', 'Griper tool cable casing', '', '2', '');

--end
