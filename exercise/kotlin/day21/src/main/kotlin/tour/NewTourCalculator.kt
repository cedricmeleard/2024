package tour

import arrow.core.Either
import arrow.core.left
import arrow.core.right
import java.time.Duration
import java.time.LocalTime

data class Step(val time: LocalTime, val label: String, val deliveryTime: Int)

class NewTourCalculator(private var steps: List<Step>) {
    private var totalDeliveryTime: Double = 0.0
    private val sortedByTimeSteps = steps.filter { true }.sortedBy { it.time }

    fun calculate(): Either<String, String> {
        if (steps.isEmpty()) {
            return "No locations !!!".left()
        }

        val result = StringBuilder()

        sortedByTimeSteps.forEach { step ->
            result.appendLine(fLine(step))
        }

        this.totalDeliveryTime = sortedByTimeSteps.sumOf { it.deliveryTime.toDouble() }

        val str: String = formatDurationToHHMMSS(totalDeliveryTime)
        result.appendLine("Delivery time | $str")

        return result.toString().right()
    }

    private fun formatDurationToHHMMSS(deliveryTime: Double): String {
        val duration = Duration.ofSeconds(
            deliveryTime.toLong()
        )
        return "${duration.toHours()}%02d:${duration.toMinutesPart()}%02d:${duration.toSecondsPart()}%02d"
    }


    private fun fLine(step: Step): String = step.let { "${it.time} : ${it.label} | ${it.deliveryTime} sec" }

}
